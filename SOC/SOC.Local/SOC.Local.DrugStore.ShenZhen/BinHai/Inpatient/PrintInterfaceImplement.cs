using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.DrugStore.ShenZhen.BinHai.Inpatient
{
    public class PrintInterfaceImplement : FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientDrug
    {

        ucTotalDrugBill ucTotalDrugBill;
        ucDetailDrugBill ucDetailDrugBill;
        ucHerbalDrugBill ucHerbalDrugBill;
        ucRecipeDrugBill ucRecipeDrugBill;
        ucDrugLabel ucDrugLabel;
        ucLZDrugBag ucLZDrugBag;
        ucCZDrugBag ucCZDrugBag;

        System.Collections.Hashtable hsDrugBillClass = new System.Collections.Hashtable();

        //配置文件和门诊的一样
        private string fileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreTotDrugBill.xml";

        #region 函数支持
        string labelPrintRegularNameFlag = "-1";

        /// <summary>
        /// 获取是否打印通用名称设置
        /// </summary>
        private bool IsLabelPrintRegularName
        {
            get
            {
                if (labelPrintRegularNameFlag != "-1")
                {
                    return FS.FrameWork.Function.NConvert.ToBoolean(labelPrintRegularNameFlag);
                }
                if (System.IO.File.Exists(fileName))
                {
                    labelPrintRegularNameFlag = SOC.Public.XML.SettingFile.ReadSetting(fileName, "Label", "ItemRegularName", "False");
                    return FS.FrameWork.Function.NConvert.ToBoolean(labelPrintRegularNameFlag);
                }
                else
                {
                    labelPrintRegularNameFlag = "False";
                    SOC.Public.XML.SettingFile.SaveSetting(fileName, "Label", "ItemRegularName", "False");
                }
                return false;
            }
        }

        string hospitalName = "";

        /// <summary>
        /// 获取医院名称
        /// </summary>
        private string HospitalName
        {
            get
            {
                if (!string.IsNullOrEmpty(hospitalName))
                {
                    return hospitalName;
                }
                if (System.IO.File.Exists(fileName))
                {
                    hospitalName = SOC.Public.XML.SettingFile.ReadSetting(fileName, "Hospital", "Name", "");
                }
                else
                {
                    SOC.Public.XML.SettingFile.SaveSetting(fileName, "Hospital", "Name", "");
                }
                return hospitalName;
            }
        }

        string memoPrintFlag = "-1";

        /// <summary>
        /// 是否打印备注
        /// </summary>
        private bool IsPrintMemo
        {
            get
            {
                if (memoPrintFlag != "-1")
                {
                    return FS.FrameWork.Function.NConvert.ToBoolean(memoPrintFlag);
                }
                if (System.IO.File.Exists(fileName))
                {
                    memoPrintFlag = SOC.Public.XML.SettingFile.ReadSetting(fileName, "Label", "PrintMemo", "False");
                    return FS.FrameWork.Function.NConvert.ToBoolean(memoPrintFlag);
                }
                else
                {
                    memoPrintFlag = "False";
                    SOC.Public.XML.SettingFile.SaveSetting(fileName, "Label", "PrintMemo", "False");
                }
                return false;
            }
        }

        string qtyShowType = "-1";
        /// <summary>
        /// 发药数量显示方式
        /// </summary>
        private string QtyShowType
        {
            get
            {
                if (qtyShowType != "-1")
                {
                    return qtyShowType;
                }
                qtyShowType = "包装单位";
                if (System.IO.File.Exists(fileName))
                {
                    return SOC.Public.XML.SettingFile.ReadSetting(fileName, "Label", "QtyShowType", "包装单位");
                }
                else
                {
                    SOC.Public.XML.SettingFile.SaveSetting(fileName, "Label", "QtyShowType", "包装单位");
                }
                return qtyShowType;
            }
        }
        #endregion

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
                    if (ucRecipeDrugBill == null)
                    {
                        ucRecipeDrugBill = new ucRecipeDrugBill();
                        ucRecipeDrugBill.Init();
                    }
                    ucRecipeDrugBill.PrintData(alData, drugBillClass, stockDept);
                }
                else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.药袋)
                {
                    this.PrintDrugBag(alData, drugBillClass, stockDept);
                }
                else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.标签)
                {
                    this.PrintDrugLabel(alData, drugBillClass, stockDept);
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

            if (alData != null)
            {
                ucDrugLabel.PrintDrugLabel(alData, this.IsLabelPrintRegularName, this.IsPrintMemo, this.QtyShowType, this.HospitalName, "");
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

            System.Collections.ArrayList alPOCZ = new System.Collections.ArrayList();
            System.Collections.ArrayList alPOLZ = new System.Collections.ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                if (!SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(applyOut.Usage.ID))
                {
                    continue;
                }
                if (applyOut.OrderType.ID == "CZ")
                {
                    alPOCZ.Add(applyOut);
                }
                else
                {
                    alPOLZ.Add(applyOut);
                }
            }
            if (ucCZDrugBag == null)
            {
                ucCZDrugBag = new ucCZDrugBag();
            }
            if (ucLZDrugBag == null)
            {
                ucLZDrugBag = new ucLZDrugBag();
            }

            if (alPOLZ.Count > 0)
            {
                ucLZDrugBag.PrintDrugBill(drugBillClass, alPOLZ, "");
            }
            if (alPOCZ.Count > 0)
            {
                ucCZDrugBag.PrintDrugBill(drugBillClass, alPOCZ, "");
            }
            return 1;
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
                    if (ucRecipeDrugBill == null)
                    {
                        ucRecipeDrugBill = new ucRecipeDrugBill();
                        ucRecipeDrugBill.Init();
                    }
                    ucRecipeDrugBill.ShowData(alData, drugBillClass, stockDept);
                    listInpatientBill.Add(ucRecipeDrugBill);
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
        /// 获取诊断
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public string GetDiagnose(string inpatientNo)
        {
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagnoseMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            ArrayList al = diagnoseMgr.QueryMainDiagnose(inpatientNo, true, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                return "查询诊断出错：";
            }
            if (al.Count == 0)
            {
                //cis传过来诊断类型全部为“15”
                al = diagnoseMgr.QueryCaseDiagnose(inpatientNo, "15", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, FS.HISFC.Models.Base.ServiceTypes.I);
                if (al == null)
                {
                    return "查询诊断出错：";
                }
                if (al.Count == 0)
                {
                    return "无";
                }
                else
                {
                    FS.HISFC.Models.HealthRecord.Diagnose diagnose = al[0] as FS.HISFC.Models.HealthRecord.Diagnose;
                    return diagnose.DiagInfo.ICD10.Name;
                }
            }
            else
            {
                FS.HISFC.Models.HealthRecord.Diagnose diagnose = al[0] as FS.HISFC.Models.HealthRecord.Diagnose;
                return diagnose.DiagInfo.ICD10.Name;
            }
        }

        #endregion
    }
}

