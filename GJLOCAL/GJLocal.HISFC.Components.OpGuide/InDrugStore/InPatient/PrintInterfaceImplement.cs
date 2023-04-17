using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient
{
    /// <summary>
    /// [功能描述: 住院药房打印本地化实例]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// 1、主要是实现接口函数逻辑
    /// </summary>>
    public class PrintInterfaceImplement : FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientDrug, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInPatientDrugTotalPrint
    {
        #region
        IBORN.ucTotalDrugBillIBORN ucTotalDrugBill;// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
        IBORN.ucDetailDrugBillIBORN ucDetailDrugBill;

        // {7DBB85BF-547C-4230-8598-55A2A4AD83F4}
        GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucHerbalDrugBillIBORN ucHerbalDrugBill;
        ucNormalDrugBill ucNormalDrugBill;
        ucDrugBagBill ucDrugBagBill;
        IBORN.ucAnestheticDrugBill ucAnestheticDrugBill;
        IBORN.ucTotalAnestheticDrugBill ucTotalAnestheticDrugBill;
        IBORN.ucReicpeInfoLabelIBORN ucReicpeInfoLabel;

        System.Collections.Hashtable hsDrugBillClass = new System.Collections.Hashtable();
        ArrayList specialDrugLabelDictionary = FS.SOC.HISFC.BizProcess.Cache.BIZManager.GetConList("SPECIALDRUGLABEL");

        #region IInpatientDrug 成员

        /// <summary>
        /// 住院药房发药保存打印调用
        /// 基于自动打印处方、摆药单、标签或者是其中几种一起打印的都在此函数中实现
        /// </summary>
        /// <param name="alData">applyout出库申请实体数组</param>
        /// <param name="drugMessage">摆药通知</param>
        /// <param name="billNO">摆药单号</param>
        /// <param name="stockDept">实发科室</param>
        /// <returns>-1发生错误 0没有处理，1处理成功</returns>
        public int OnSavePrint(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugMessage drugMessage, string billNO, FS.FrameWork.Models.NeuObject stockDept)
        {
            FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            drugBillClass.ID = drugMessage.DrugBillClass.ID;
            drugBillClass.Name = drugMessage.DrugBillClass.Name;
            drugBillClass.ApplyState = "0";
            drugBillClass.DrugBillNO = billNO;
            drugBillClass.ApplyDept.ID = drugMessage.ApplyDept.ID;
            drugBillClass.ApplyDept.Name = drugMessage.ApplyDept.Name;


            //明细显示考虑具体情况：草药需要草药处方格式，出院带药、请假带药可能需要住院处方格式
            List<FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType> listBillType = this.GetBillType(drugBillClass);

            foreach (FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType billType in listBillType)
            {
                //先处理汇总显示
                if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.汇总)
                {
                    if (ucTotalDrugBill == null)// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
                    {
                        ucTotalDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucTotalDrugBillIBORN();
                        ucTotalDrugBill.Init();
                    }
                    ucTotalDrugBill.PrintData(alData, drugBillClass, stockDept);
                    //药袋用作爱博恩特殊需求 CJF 2017年5月3日
                    //1、口服针剂分开，口服打印药袋，针剂打印明细
                    //2、口服按使用时间分组，每组打印一个药袋
                    //3、特殊药品根据itemid进行特殊带标签 HYG 2020年5月28
                    ArrayList alInjust = new ArrayList();
                    ArrayList alPO = new ArrayList();
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut obj in alData)
                    {
                        if (FS.SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(obj.Usage.ID))
                        {
                            alInjust.Add(obj);
                            if (isPrintSpecialDrugLabel(obj.Item.ID))
                            {
                                alPO.Add(obj);
                            }
                        }
                        //{C563CFE3-40E4-4662-81D4-4A2CBF89015D}
                        else if (!(FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugDoseModual(obj.Item.DosageForm.ID)).Name.Contains("注射"))
                        {
                            alPO.Add(obj);
                        }
                        else if (isPrintSpecialDrugLabel(obj.Item.ID))
                        {
                            //{C5708768-36D8-4425-9B26-7E9AD7CE98B6}
                            //DialogResult dr = MessageBox.Show(obj.Name+"为特殊药品，是否自动打印标签？", "提示", MessageBoxButtons.YesNo);
                            //if (dr == DialogResult.Yes)
                            //{
                                alPO.Add(obj);
                            //}
                        }
                    }
                    #region 口服标签
                    if (alPO.Count > 0 && drugBillClass.Name!="退药单")
                    {
                        //待整理列表中，口服和外用的自动进行收录到alPO，然后进行list的标签打印
                        this.PrintDrugLabel(alPO, drugBillClass, stockDept);
                    }
                    #endregion
                }
                else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.明细)
                {
                    //药袋用作爱博恩特殊需求 CJF 2017年5月3日
                    //1、口服针剂分开，口服打印药袋，针剂打印明细
                    //2、口服按使用时间分组，每组打印一个药袋
                    ArrayList alInjust = new ArrayList();
                    ArrayList alPO = new ArrayList();
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut obj in alData)
                    {
                        //{738625BE-10F2-41cf-AC76-A2A1AA54307F}
                        //{83DABC8A-E421-45ea-85DC-4C28B97BBBE1}
                        if (drugBillClass.ID == "S" || drugBillClass.ID == "QS" || drugBillClass.ID == "ZDP2")
                        {
                            alInjust.Add(obj);
                        }
                        else if (!string.IsNullOrEmpty(obj.Usage.ID)&& FS.SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(obj.Usage.ID))
                        {
                            alInjust.Add(obj);
                        }
                        else if(!(FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugDoseModual(obj.Item.DosageForm.ID)).Name.Contains("注射"))
                        {
                            alPO.Add(obj);
                        }
                    }

                    #region 针剂打印明细
                    if (alInjust.Count > 0)
                    {
                        if (ucDetailDrugBill == null)// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
                        {
                            ucDetailDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucDetailDrugBillIBORN();
                            ucDetailDrugBill.Init();
                        }
                        if (drugBillClass.ID == "P" || drugBillClass.ID == "OP1" || drugBillClass.ID == "S" || drugBillClass.ID == "QS" || drugBillClass.ID == "ZDP2")// {F417D766-19C0-4d3e-AB72-D774058B497E}
                        {
                            ArrayList allNormal = new ArrayList();
                            ArrayList allAnes = new ArrayList();
                            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in alInjust)
                            {
                                if (FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyInfo.Item.Quality.ID) == "S"
                                    || FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyInfo.Item.Quality.ID) == "P"
                                    || FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyInfo.Item.Quality.ID) == "Q"
                                    || FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyInfo.Item.Quality.ID) == "P2")              
                                {
                                    allAnes.Add(applyInfo);
                                }
                                else
                                {
                                    allNormal.Add(applyInfo);
                                }
                            }
                            if (allAnes.Count > 0)// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
                            {
                                ucAnestheticDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucAnestheticDrugBill();
                                ucAnestheticDrugBill.Init();
                                ucAnestheticDrugBill.PrintData(allAnes, drugBillClass, stockDept);

                                //{CA91E91E-54C5-4afd-B44D-E9114D53172B}

                                ucTotalAnestheticDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucTotalAnestheticDrugBill();
                                ucTotalAnestheticDrugBill.Init();
                                ucTotalAnestheticDrugBill.PrintData(allAnes, drugBillClass, stockDept);
                            }
                            if (allNormal.Count > 0)
                            {
                                ucDetailDrugBill.PrintData(allNormal, drugBillClass, stockDept);
                            }
                        }
                        else
                        {
                            ucDetailDrugBill.PrintData(alInjust, drugBillClass, stockDept);
                        }
                    }
                    #endregion

                    #region 口服打印药袋
                    if (alPO.Count > 0)
                    {
                        UsageComparer usageComparer = new UsageComparer();
                        alPO.Sort(usageComparer);
                        //按使用患者、使用时间分组（同一个人同一执行时间时需要打印在一起）
                        ArrayList alTime = new ArrayList();
                        ArrayList alAll = new ArrayList();
                        DateTime dt = DateTime.Now;
                        string strPersonAndTime = "";
                        foreach (FS.HISFC.Models.Pharmacy.ApplyOut obj in alPO)
                        {
                            //{DDFC27A3-159F-4558-96D7-55BB812E44E4}
                            if (strPersonAndTime != (obj.PatientNO + obj.UseTime.Date.ToString() + obj.UseTime.ToShortTimeString()))
                            {
                                if (alTime.Count > 0)
                                {
                                    alAll.Add(alTime.Clone());
                                    alTime = new ArrayList();
                                }
                                alTime.Add(obj);
                                strPersonAndTime = (obj.PatientNO + obj.UseTime.Date.ToString() + obj.UseTime.ToShortTimeString());
                            }
                            else
                            {
                                alTime.Add(obj);
                            }
                        }
                        if (alTime.Count > 0)
                        {
                            //{01BE5122-CBDC-4e2d-9641-34EEE5D02DE5}
                            alAll.Add(alTime);
                        }

                        ArrayList alTemp = new ArrayList();
                        for (int i = 0; i < alAll.Count;i++ )
                        {
                            alTemp = new ArrayList();
                            alTemp = alAll[i] as ArrayList;
                            if (alTemp.Count > 0)
                            {
                                ucDrugBagBill = new ucDrugBagBill();
                                ucDrugBagBill.Init();
                                ucDrugBagBill.PrintData(alTemp, drugBillClass, stockDept);
                            }
                        }

                        if (ucDetailDrugBill == null)// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
                        {
                            ucDetailDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucDetailDrugBillIBORN();
                            ucDetailDrugBill.Init();
                        }
                        ucDetailDrugBill.PrintData(alPO, drugBillClass, stockDept);
                    }
                    #endregion

                    #region 作废
                    //if (ucDetailDrugBill == null)
                    //{
                    //    ucDetailDrugBill = new ucDetailDrugBill();
                    //    ucDetailDrugBill.Init();
                    //}
                    //if (drugBillClass.ID == "P")
                    //{
                    //    ArrayList allNormal = new ArrayList();
                    //    ArrayList allAnes = new ArrayList();
                    //    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in alData)
                    //    {
                    //        if (FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyInfo.Item.Quality.ID) == "S" || FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyInfo.Item.Quality.ID) == "P")
                    //        {
                    //            allAnes.Add(applyInfo);
                    //        }
                    //        else
                    //        {
                    //            allNormal.Add(applyInfo);
                    //        }
                    //    }
                    //    if (allAnes.Count > 0)
                    //    {
                    //        ucAnestheticDrugBill = new ucAnestheticDrugBill();
                    //        ucAnestheticDrugBill.Init();
                    //        ucAnestheticDrugBill.PrintData(allAnes, drugBillClass, stockDept);
                    //    }
                    //    if(allNormal.Count > 0)
                    //    {
                    //        ucDetailDrugBill.PrintData(allNormal, drugBillClass, stockDept);
                    //    }
                    //}
                    //else
                    //{
                    //    ucDetailDrugBill.PrintData(alData, drugBillClass, stockDept);
                    //}
                    #endregion

                }
                else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.草药)
                {
                    if (ucHerbalDrugBill == null)// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
                    {
                        ucHerbalDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucHerbalDrugBillIBORN();
                        ucHerbalDrugBill.Init();
                    }

                    ucHerbalDrugBill.PrintData(alData, drugBillClass, stockDept);

                }
                else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.出院带药处方)
                {
                    if (drugBillClass.ID == "OP1" || drugBillClass.ID == "S")// {F417D766-19C0-4d3e-AB72-D774058B497E}
                    {
                        if (ucAnestheticDrugBill == null)
                        {
                            ucAnestheticDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucAnestheticDrugBill();
                            ucAnestheticDrugBill.Init();
                        }
                        ucAnestheticDrugBill.PrintData(alData, drugBillClass, stockDept);
                    }
                    else
                    {
                        if (ucDetailDrugBill == null)// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
                        {
                            ucDetailDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucDetailDrugBillIBORN();
                            ucDetailDrugBill.Init();
                        }
                        //ucNormalDrugBill.PrintData(alData, drugBillClass, stockDept);

                        ucDetailDrugBill.PrintData(alData, drugBillClass, stockDept);
                    }

                    //药袋用作爱博恩特殊需求 CJF 2017年5月3日
                    //1、口服针剂分开，口服打印药袋，针剂打印明细
                    //2、口服按使用时间分组，每组打印一个药袋
                    ArrayList alInjust = new ArrayList();
                    ArrayList alPO = new ArrayList();
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut obj in alData)
                    {
                        if (FS.SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(obj.Usage.ID))
                        {
                            alInjust.Add(obj);
                        }
                        else
                        {
                            alPO.Add(obj);
                        }
                    }
                    #region 口服打印药袋
                    if (alPO.Count > 0)
                    {
                        UsageComparer usageComparer = new UsageComparer();
                        alPO.Sort(usageComparer);
                        //按使用患者、使用时间分组（同一个人同一执行时间时需要打印在一起）
                        ArrayList alTime = new ArrayList();
                        ArrayList alAll = new ArrayList();
                        DateTime dt = DateTime.Now;
                        string strPersonAndTime = "";
                        foreach (FS.HISFC.Models.Pharmacy.ApplyOut obj in alPO)
                        {
                            if (strPersonAndTime != (obj.PatientNO + obj.UseTime.ToString()))
                            {
                                if (alTime.Count > 0)
                                {
                                    alAll.Add(alTime.Clone());
                                    alTime = new ArrayList();
                                }
                                alTime.Add(obj);
                                strPersonAndTime = (obj.PatientNO + obj.UseTime.ToString());
                            }
                            else
                            {
                                alTime.Add(obj);
                            }
                        }
                        if (alTime.Count > 0)
                        {
                            //{01BE5122-CBDC-4e2d-9641-34EEE5D02DE5}
                            alAll.Add(alTime);
                        }

                        ArrayList alTemp = new ArrayList();
                        for (int i = 0; i < alAll.Count; i++)
                        {
                            alTemp = new ArrayList();
                            alTemp = alAll[i] as ArrayList;
                            if (alTemp.Count > 0)
                            {
                                ucDrugBagBill = new ucDrugBagBill();
                                ucDrugBagBill.Init();
                                ucDrugBagBill.PrintData(alTemp, drugBillClass, stockDept);
                            }
                        }
                    }
                    #endregion
                }
                else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.药袋)
                {
                    
                }
                else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.标签)
                {

                }
            }

            return 0;
        }

        /// <summary>
        /// 标签打印
        /// 一般在手工打印或补打时调用
        /// </summary>
        /// <param name="alData">applyout出库申请实体数组</param>
        /// <param name="drugBillClass">摆药单分类</param>
        /// <param name="stockDept">实发科室</param>
        /// <returns>-1发生错误 0没有处理，1处理成功</returns>
        public int PrintDrugLabel(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            if (alData.Count > 0)// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
            {
                ucReicpeInfoLabel = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucReicpeInfoLabelIBORN();
                ucReicpeInfoLabel.PrintDrugLabel(alData, drugBillClass, stockDept);
            }
            else
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 药袋打印
        /// 一般在手工打印或补打时调用
        /// </summary>
        /// <param name="alData">applyout出库申请实体数组</param>
        /// <param name="drugBillClass">摆药单分类</param>
        /// <param name="stockDept">实发科室</param>
        /// <returns>-1发生错误 0没有处理，1处理成功</returns>
        public int PrintDrugBag(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            //药袋用作爱博恩特殊需求 CJF 2017年5月3日
            //1、口服针剂分开，口服打印药袋，针剂打印明细
            //2、口服按使用时间分组，每组打印一个药袋
            ArrayList alInjust = new ArrayList();
            ArrayList alPO = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut obj in alData)
            {
                if (FS.SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(obj.Usage.ID))
                {
                    alInjust.Add(obj);
                }
                else
                {
                    alPO.Add(obj);
                }
            }

            #region 针剂打印明细
            if (alInjust.Count > 0)
            {
                if (ucDetailDrugBill == null)// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
                {
                    ucDetailDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucDetailDrugBillIBORN();
                    ucDetailDrugBill.Init();
                }
                if (drugBillClass.ID == "P")
                {
                    ArrayList allNormal = new ArrayList();
                    ArrayList allAnes = new ArrayList();
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in alInjust)
                    {
                        if (FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyInfo.Item.Quality.ID) == "S"
                            || FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyInfo.Item.Quality.ID) == "P")
                        {
                            allAnes.Add(applyInfo);
                        }
                        else
                        {
                            allNormal.Add(applyInfo);
                        }
                    }
                    if (allAnes.Count > 0)// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
                    {
                        ucAnestheticDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucAnestheticDrugBill();
                        ucAnestheticDrugBill.Init();
                        ucAnestheticDrugBill.PrintData(allAnes, drugBillClass, stockDept);
                    }
                    if (allNormal.Count > 0)
                    {
                        ucDetailDrugBill.PrintData(allNormal, drugBillClass, stockDept);
                    }
                }
                else
                {
                    ucDetailDrugBill.PrintData(alInjust, drugBillClass, stockDept);
                }
            }
            #endregion

            #region 口服打印药袋
            if (alPO.Count > 0)
            {
                UsageComparer usageComparer = new UsageComparer();
                alPO.Sort(usageComparer);
                //按使用患者、使用时间分组（同一个人同一执行时间时需要打印在一起）
                ArrayList alTime = new ArrayList();
                ArrayList alAll = new ArrayList();
                DateTime dt = DateTime.Now;
                string strPersonAndTime = "";
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut obj in alPO)
                {
                    if (strPersonAndTime != (obj.PatientNO + obj.UseTime.Date.ToString()+ obj.UseTime.ToShortTimeString()))
                    {
                        if (alTime.Count > 0)
                        {
                            alAll.Add(alTime.Clone());
                            alTime = new ArrayList();
                        }
                        alTime.Add(obj);
                        strPersonAndTime = (obj.PatientNO + obj.UseTime.Date.ToString()+ obj.UseTime.ToShortTimeString());
                    }
                    else
                    {
                        alTime.Add(obj);
                    }
                }
                if (alTime.Count > 0)
                {
                    alAll.Add(alTime);
                }

                foreach (ArrayList alTemp in alAll)
                {
                    if (alTemp.Count > 0)
                    {
                        ucDrugBagBill = new ucDrugBagBill();
                        ucDrugBagBill.Init();
                        ucDrugBagBill.PrintData(alTemp, drugBillClass, stockDept);
                    }
                }
            }
            #endregion
            return 0;
        }

        /// <summary>
        /// 药房的单据打印：汇总和明细
        /// </summary>
        /// <param name="alData">出库申请applyout</param>
        /// <param name="drugBillClass">摆药单分类</param>
        /// <param name="stockDept">实际扣库库存科室</param>
        /// <returns>显示单据的控件</returns>
        public List<FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill> ShowDrugBill(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            if (alData == null || alData.Count == 0)
            {
                return null;
            }

            List<FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill> listInpatientBill = new List<FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill>();

            //明细显示考虑具体情况：草药需要草药处方格式，出院带药、请假带药可能需要住院处方格式
            List<FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType> listBillType = this.GetBillType(drugBillClass);

            foreach (FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType billType in listBillType)
            {
                //先处理汇总显示
                if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.汇总)
                {
                    if (ucTotalDrugBill == null)// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
                    {
                        ucTotalDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucTotalDrugBillIBORN();
                        ucTotalDrugBill.Init();
                    }
                    ucTotalDrugBill.ShowData(alData, drugBillClass, stockDept);
                    listInpatientBill.Add(ucTotalDrugBill);
                }
                else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.明细)
                {
                    if (drugBillClass.ID == "OP1" || drugBillClass.ID == "S" || drugBillClass.ID == "QS" || drugBillClass.ID == "ZDP2")
                    {
                        if (ucAnestheticDrugBill == null)// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
                        {
                            ucAnestheticDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucAnestheticDrugBill();
                            ucAnestheticDrugBill.Init();
                        }
                        ucAnestheticDrugBill.ShowData(alData, drugBillClass, stockDept);
                        listInpatientBill.Add(ucAnestheticDrugBill);
                    }
                    else
                    {
                        if (ucDetailDrugBill == null)// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
                        {
                            ucDetailDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucDetailDrugBillIBORN();
                            ucDetailDrugBill.Init();
                        }

                        ucDetailDrugBill.ShowData(alData, drugBillClass, stockDept);
                        listInpatientBill.Add(ucDetailDrugBill);
                    }

                }
                else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.草药)
                {
                    if (ucHerbalDrugBill == null)// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
                    {
                        ucHerbalDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucHerbalDrugBillIBORN();
                        ucHerbalDrugBill.Init();
                    }

                    ucHerbalDrugBill.ShowData(alData, drugBillClass, stockDept);
                    listInpatientBill.Add(ucHerbalDrugBill);

                }
                else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.出院带药处方)
                {
                    if (drugBillClass.ID == "OP1" || drugBillClass.ID == "S")
                    {
                        if (ucAnestheticDrugBill == null)// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
                        {
                            ucAnestheticDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucAnestheticDrugBill();
                            ucAnestheticDrugBill.Init();
                        }
                        ucAnestheticDrugBill.ShowData(alData, drugBillClass, stockDept);
                        listInpatientBill.Add(ucAnestheticDrugBill);
                    }
                    else
                    {
                        //if (ucNormalDrugBill == null)
                        //{
                        //    ucNormalDrugBill = new ucNormalDrugBill();
                        //    ucNormalDrugBill.Init();
                        //}
                        //ucNormalDrugBill.ShowData(alData, drugBillClass, stockDept);
                        //listInpatientBill.Add(ucNormalDrugBill);

                        if (ucDetailDrugBill == null)// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
                        {
                            ucDetailDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucDetailDrugBillIBORN();
                            ucDetailDrugBill.Init();
                        }

                        ucDetailDrugBill.ShowData(alData, drugBillClass, stockDept);
                        listInpatientBill.Add(ucDetailDrugBill);
                    }
                }
            }
            return listInpatientBill;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 根据摆药单类型ID获取打印单据的类别（明细、汇总、出院带药、草药）
        /// </summary>
        /// <param name="drugBillClass">摆药单类型</param>
        /// <returns></returns>
        private List<FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType> GetBillType(FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            List<FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType> listBillType = new List<FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType>();

            if (hsDrugBillClass.Count == 0)
            {
                FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();
                System.Collections.ArrayList alDrugBillClass = drugStoreMgr.QueryDrugBillClassList();
                if (alDrugBillClass == null || alDrugBillClass.Count == 0)
                {
                    System.Windows.Forms.MessageBox.Show("没有找到摆药单分类，打印单据将按照明细格式打印。有疑问请与系统管理员联系！");
                    listBillType.Add(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.明细);
                    return listBillType;
                }

                foreach (FS.HISFC.Models.Pharmacy.DrugBillClass info in alDrugBillClass)
                {
                    if (hsDrugBillClass.Contains(info.ID))
                    {
                        continue;
                    }
                    hsDrugBillClass.Add(info.ID, info);
                }
            }


            if (hsDrugBillClass.Contains(drugBillClass.ID))
            {
                FS.HISFC.Models.Pharmacy.DrugBillClass tmpDrugBillClass = hsDrugBillClass[drugBillClass.ID] as FS.HISFC.Models.Pharmacy.DrugBillClass;
                drugBillClass.PrintType = tmpDrugBillClass.PrintType;
                drugBillClass.Name = tmpDrugBillClass.Name;
            }

            switch (drugBillClass.PrintType.ID.ToString())
            {
                case "T":
                    listBillType.Add(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.汇总);
                    break;
                case "D":
                    if (Common.Function.IsPrintInpatientDrugTot(drugBillClass.ID))
                    {
                        listBillType.Add(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.汇总);
                    }
                    if (Common.Function.IsPrintInpatientDrugBag(drugBillClass.ID))
                    {
                        listBillType.Add(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.药袋);
                    }
                    listBillType.Add(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.明细);
                    break;
                case "H":
                    listBillType.Add(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.草药);
                    break;
                case "R":
                    listBillType.Add(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.出院带药处方);
                    if (Common.Function.IsPrintInpatientDrugLabel(drugBillClass.ID))
                    {
                        listBillType.Add(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.标签);
                    }
                    break;
                default:
                    break;
            }

            return listBillType;
        }

        /// <summary>
        /// 按用药时间/当前时间 组合显示
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sysdate"></param>
        /// <returns></returns>
        private string FormatDateTime(DateTime dt, DateTime sysdate)
        {
            try
            {
                if (sysdate.Date.AddDays(-1) == dt.Date)
                {
                    return "昨" + dt.Hour.ToString().PadLeft(2, '0');
                }
                else if (sysdate.Date == dt.Date)
                {
                    return "今" + dt.Hour.ToString().PadLeft(2, '0');
                }
                else if (sysdate.Date.AddDays(1) == dt.Date)
                {
                    return "明" + dt.Hour.ToString().PadLeft(2, '0');
                }
                else if (sysdate.Date.AddDays(2) == dt.Date)
                {
                    return "后" + dt.Hour.ToString().PadLeft(2, '0');
                }
                else
                {
                    if (dt.Month == sysdate.Month)
                    {
                        return dt.Day.ToString().PadLeft(2, '0') + dt.Hour.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        return dt.Month.ToString().PadLeft(2, '0') + dt.Day.ToString().PadLeft(2, '0') + dt.Hour.ToString().PadLeft(2, '0');

                    }
                }
            }
            catch
            {
                return dt.Month.ToString().PadLeft(2, '0') + dt.Day.ToString().PadLeft(2, '0') + dt.Hour.ToString().PadLeft(2, '0');
            }
        }

        private bool isPrintSpecialDrugLabel(string id)
        {
            if(specialDrugLabelDictionary==null||specialDrugLabelDictionary.Count<=0)
            {
                return false;
            }

            foreach(Object o in specialDrugLabelDictionary)
            {
                FS.FrameWork.Models.NeuObject neu = (FS.FrameWork.Models.NeuObject) o;
                if(neu.ID==id)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        #endregion

        #region IInPatientDrugTotalPrint 成员

        public int PrintDrugBillDetail(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            if (stockDept.User01 == "打印药袋")//打印药袋接口没有实现，为了不改变原接口，暂用USER01
            {
                return this.PrintDrugBag(alData, drugBillClass, stockDept);
            }
            else
            {
                if (alData == null || alData.Count == 0)// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
                {
                    return -1;
                }
                ucDetailDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucDetailDrugBillIBORN();

                ////患者和申请信息对应
                //Hashtable hsApplyInfo = new Hashtable();

                //ArrayList difPatient = new ArrayList();
                ////按患者分组申请信息
                //foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in alData)
                //{
                //    if (hsApplyInfo.Contains(applyInfo.PatientNO))
                //    {
                //        ArrayList alPatientData = hsApplyInfo[applyInfo.PatientNO] as ArrayList;
                //        alPatientData.Add(applyInfo);
                //    }
                //    else
                //    {
                //        ArrayList alPatientData = new ArrayList();
                //        alPatientData.Add(applyInfo);
                //        hsApplyInfo.Add(applyInfo.PatientNO, alPatientData);
                //        difPatient.Add(applyInfo.PatientNO);
                //    }
                //}

                ucDetailDrugBill.PrintData(alData, drugBillClass, stockDept);// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
                ////循环打印明细单据
                //foreach (string patintid in difPatient)
                //{
                //    ArrayList patientData = hsApplyInfo[patintid] as ArrayList;
                //    ucDetailDrugBill.PrintData(patientData, drugBillClass, stockDept);
                //}

                return 1;
            }
        }

        public int PrintDrugBillTotal(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            if (alData == null || alData.Count == 0)
            {
                return -1;
            }
            if (ucTotalDrugBill == null)// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
            {
                ucTotalDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucTotalDrugBillIBORN();
                ucTotalDrugBill.Init();
            }
            ucTotalDrugBill.PrintData(alData, drugBillClass, stockDept);
            return 1;
        }

        #endregion
    }

    public class UsageComparer : System.Collections.IComparer
    {
        int System.Collections.IComparer.Compare(object x, object y)
        {
            FS.HISFC.Models.Pharmacy.ApplyOut applyInfoX = (FS.HISFC.Models.Pharmacy.ApplyOut)x;
            FS.HISFC.Models.Pharmacy.ApplyOut applyInfoY = (FS.HISFC.Models.Pharmacy.ApplyOut)y;

            if (string.Compare(applyInfoX.PatientNO , applyInfoY.PatientNO)>0)
                return 1;
            if (string.Compare(applyInfoX.PatientNO , applyInfoY.PatientNO)<0)
                return -1;

            if (string.Compare(applyInfoX.UseTime.Date.ToString(), applyInfoY.UseTime.Date.ToString()) > 0)
                return 1;
            if (string.Compare(applyInfoX.UseTime.Date.ToString(), applyInfoY.UseTime.Date.ToString()) < 0)
                return -1;

            if (string.Compare(applyInfoX.UseTime.ToString(), applyInfoY.UseTime.ToString()) > 0)
                return 1;
            if (string.Compare(applyInfoX.UseTime.ToString(), applyInfoY.UseTime.ToString()) < 0)
                return -1;

            if (string.Compare(applyInfoX.Item.ID , applyInfoY.Item.ID)>0)
                return 1;
            if (string.Compare(applyInfoX.Item.ID ,applyInfoY.Item.ID)<0)
                return -1;
            return 0;
        }
    }
}

