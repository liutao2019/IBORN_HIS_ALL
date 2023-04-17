using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.DrugStore.GuangZhou.ZDLY.Inpatient
{
    /// <summary>
    /// [功能描述: 住院药房打印本地化实例]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// 1、主要是实现接口函数逻辑
    /// </summary>>
    public class PrintInterfaceImplement : FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientDrug
    {
        ucTotalDrugBill ucTotalDrugBill;
        ucDetailDrugBill ucDetailDrugBill;
        ucHerbalDrugBill ucHerbalDrugBill;
        ucNormalDrugBill ucNormalDrugBill;
        ucInpatientRecipe ucInpatientRecipe;
        ucDrugLabel ucDrugLabel;

        System.Collections.Hashtable hsDrugBillClass = new System.Collections.Hashtable();

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
            List<SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType> listBillType = this.GetBillType(drugBillClass);

            foreach (SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType billType in listBillType)
            {
                //先处理汇总显示
                if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.汇总)
                {
                    if (ucTotalDrugBill == null)
                    {
                        ucTotalDrugBill = new ucTotalDrugBill();
                        ucTotalDrugBill.Init();
                    }
                    ucTotalDrugBill.PrintData(alData, drugBillClass, stockDept);
                }
                else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.明细)
                {
                    if (ucDetailDrugBill == null)
                    {
                        ucDetailDrugBill = new ucDetailDrugBill();
                        ucDetailDrugBill.Init();
                    }

                    ucDetailDrugBill.PrintData(alData, drugBillClass, stockDept);

                }
                else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.草药)
                {
                    if (ucHerbalDrugBill == null)
                    {
                        ucHerbalDrugBill = new ucHerbalDrugBill();
                        ucHerbalDrugBill.Init();
                    }

                    ucHerbalDrugBill.PrintData(alData, drugBillClass, stockDept);

                }
                else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.出院带药处方)
                {
                    //if (ucNormalDrugBill == null)
                    //{
                    //    ucNormalDrugBill = new ucNormalDrugBill();
                    //    ucNormalDrugBill.Init();
                    //}
                    //ucNormalDrugBill.PrintData(alData, drugBillClass, stockDept);



                    if (ucInpatientRecipe == null)
                    {
                        ucInpatientRecipe = new ucInpatientRecipe();
                        ucInpatientRecipe.Init();
                    }
                    ucInpatientRecipe.PrintData(alData, drugBillClass, stockDept);

                    if (ucDrugLabel==null)
                    {
                        ucDrugLabel = new ucDrugLabel();
                    }
                    ucDrugLabel.PrintDrugLabel(alData, drugBillClass, stockDept);
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
            if (ucDrugLabel == null)
            {
                ucDrugLabel = new ucDrugLabel();
            }
            return ucDrugLabel.PrintDrugLabel(alData, drugBillClass, stockDept);
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
            return 0;
        }

        /// <summary>
        /// 药房的单据打印：汇总和明细
        /// </summary>
        /// <param name="alData">出库申请applyout</param>
        /// <param name="drugBillClass">摆药单分类</param>
        /// <param name="stockDept">实际扣库库存科室</param>
        /// <returns>显示单据的控件</returns>
        public List<SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill> ShowDrugBill(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            if (alData == null || alData.Count == 0)
            {
                return null;
            }

            List<SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill> listInpatientBill = new List<FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill>();

            //明细显示考虑具体情况：草药需要草药处方格式，出院带药、请假带药可能需要住院处方格式
            List<SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType> listBillType = this.GetBillType(drugBillClass);

            foreach (SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType billType in listBillType)
            {
                //先处理汇总显示
                if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.汇总)
                {
                    if (ucTotalDrugBill == null)
                    {
                        ucTotalDrugBill = new ucTotalDrugBill();
                        ucTotalDrugBill.Init();
                    }
                    ucTotalDrugBill.ShowData(alData, drugBillClass, stockDept);
                    listInpatientBill.Add(ucTotalDrugBill);
                }
                else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.明细)
                {
                    if (ucDetailDrugBill == null)
                    {
                        ucDetailDrugBill = new ucDetailDrugBill();
                        ucDetailDrugBill.Init();
                    }

                    ucDetailDrugBill.ShowData(alData, drugBillClass, stockDept);
                    listInpatientBill.Add(ucDetailDrugBill);

                }
                else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.草药)
                {
                    if (ucHerbalDrugBill == null)
                    {
                        ucHerbalDrugBill = new ucHerbalDrugBill();
                        ucHerbalDrugBill.Init();
                    }

                    ucHerbalDrugBill.ShowData(alData, drugBillClass, stockDept);
                    listInpatientBill.Add(ucHerbalDrugBill);

                }
                else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.出院带药处方)
                {

                    if (ucInpatientRecipe == null)
                    {
                        ucInpatientRecipe = new ucInpatientRecipe();
                        ucInpatientRecipe.Init();
                    }
                    ucInpatientRecipe.ShowData(alData, drugBillClass, stockDept);
                    listInpatientBill.Add(ucInpatientRecipe);
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
        private List<SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType> GetBillType(FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            List<SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType> listBillType = new List<FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType>();

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
        /// 获取住院标签打印数据
        /// </summary>
        /// <param name="allData"></param>
        /// <returns></returns>
        private System.Collections.ArrayList GetInptientDrugLabelData(System.Collections.ArrayList allData)
        {
            System.Collections.ArrayList inpatientDrugLabelData = new System.Collections.ArrayList();
            if (allData == null || allData.Count == 0)
            {
                return null;
            }


            for (int i = 0; i < allData.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut drugLabelData = allData[i] as FS.HISFC.Models.Pharmacy.ApplyOut;
                //过滤草药不打印
                if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSysClass(drugLabelData.Item.ID) != "PCC")
                {
                    inpatientDrugLabelData.Add(drugLabelData);
                }
            }
            return inpatientDrugLabelData;
        }

        #endregion
    }
}

