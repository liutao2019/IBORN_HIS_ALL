using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.DrugStore.FuYou.Inpatient
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
        ucLZDrugBag ucLZDrugBag;
        ucCZDrugBag ucCZDrugBag;


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
                    if (ucNormalDrugBill == null)
                    {
                        ucNormalDrugBill = new ucNormalDrugBill();
                        ucNormalDrugBill.Init();
                    }
                    ucNormalDrugBill.PrintData(alData, drugBillClass, stockDept);
                }
                else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.药袋)
                {
                    this.PrintDrugBagBase(alData, drugBillClass, stockDept, false);
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
            return 0;
        }


        /// <summary>
        /// 住院药房上班时间 （用于药袋打印)
        /// </summary>
        System.Collections.ArrayList alDrugStoreWorkTime = null;

        FS.SOC.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 统一调用的药袋打印方法
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        /// <param name="stockDept"></param>
        /// <returns></returns>
        private int PrintDrugBagBase(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept,bool isReprint)
        {
            //---------------------------------------------------------------------
            //修改此处要注意！ 修改打印规则要对应收取规则
            //SOC.Local.Order.SubFeeSet.SDFY.SubFeeSet
            //GetOrderListForDrugBag方法
            //---------------------------------------------------------------------


            DateTime dtNow = this.itemMgr.GetDateTimeFromSysDateTime();

            System.Collections.ArrayList alPOCZ = new System.Collections.ArrayList();
            System.Collections.ArrayList alPOLZ = new System.Collections.ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                //特殊处理
                //妇幼中心药房晚上不上班，在中心药房上班前患者用的药品一般科室自己给患者用药（科室库存或者借药），这样第二天药房就不需要打印昨天或上班前的药袋
                if (alDrugStoreWorkTime == null)
                {
                    alDrugStoreWorkTime = new System.Collections.ArrayList();
                    FS.HISFC.BizProcess.Integrate.Manager inteMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                    alDrugStoreWorkTime = inteMgr.GetConstantList("DrugStoreWorkTime");
                }

                if (!isReprint)
                {
                    DateTime dtDrugStoreWorkTime = DateTime.MinValue;

                    try
                    {
                        foreach (FS.HISFC.Models.Base.Const con in alDrugStoreWorkTime)
                        {
                            if (con.IsValid)
                            {
                                dtDrugStoreWorkTime = dtNow.Date + FS.FrameWork.Function.NConvert.ToDateTime(con.Name).TimeOfDay;
                            }
                        }
                    }
                    catch
                    {
                        dtDrugStoreWorkTime = DateTime.MinValue;
                    }

                    if (applyOut.UseTime < dtDrugStoreWorkTime)
                    {
                        continue;
                    }
                }

                //特殊处理的地方：
                //    1）、住院小儿科的都按照临嘱格式打印
                //    2）、住院的剂型 02 注射剂；03 口服溶液剂 都是按照临嘱格式打印

                if (applyOut.ApplyDept.ID == "0701"//住院小儿科
                    || applyOut.ApplyDept.ID == "8255"//儿科一区护士站
                    || applyOut.Item.DosageForm.ID == "02"//注射剂
                    || applyOut.Item.DosageForm.ID == "03"//口服溶液剂
                   )
                {
                    alPOLZ.Add(applyOut);
                }
                else
                {
                    if (applyOut.OrderType.ID == "CZ")
                    {
                        alPOCZ.Add(applyOut);
                    }
                    else
                    {
                        alPOLZ.Add(applyOut);
                    }
                }
            }
            if (ucLZDrugBag == null)
            {
                ucLZDrugBag = new ucLZDrugBag();
            }
            if (ucCZDrugBag == null)
            {
                ucCZDrugBag = new ucCZDrugBag();
            }
            ucLZDrugBag.PrintDrugBill(drugBillClass, alPOLZ, "");
            ucCZDrugBag.PrintDrugBill(drugBillClass, alPOCZ, "");
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
            return this.PrintDrugBagBase(alData, drugBillClass, stockDept, true);
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
                    if (ucNormalDrugBill == null)
                    {
                        ucNormalDrugBill = new ucNormalDrugBill();
                        ucNormalDrugBill.Init();
                    }
                    ucNormalDrugBill.ShowData(alData, drugBillClass, stockDept);
                    listInpatientBill.Add(ucNormalDrugBill);
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

        #endregion
    }
}

