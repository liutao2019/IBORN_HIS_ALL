using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Neusoft.SOC.Local.DrugStore.FuYou.Outpatient
{
    /// <summary>
    /// [功能描述: 妇幼门诊药房打印本地化实例]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-1]<br></br>
    /// 说明：
    /// 1、主要是实现接口函数逻辑
    /// 2、不要相信一个处方只有5个药品，任何情况都考虑分页的可能
    /// 3、不要相信一个终端只有一种药品类别，任何终端都有可能有两种或者两种以上类别
    /// 4、不要相信一个终端只有一种打印方式，考虑药品类别对应打印方式是比较合理的
    /// 5、任何情况下都考虑有作废的数据
    /// </summary>>
    public class PrintMaitenance : Neusoft.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientDrug
    {
        private string fileName = Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugStorePrintSetting.xml";

        Neusoft.HISFC.BizLogic.HealthRecord.Diagnose diagnoseMgr = new Neusoft.HISFC.BizLogic.HealthRecord.Diagnose();

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
                    return Neusoft.FrameWork.Function.NConvert.ToBoolean(labelPrintRegularNameFlag);
                }
                if (System.IO.File.Exists(fileName))
                {
                    labelPrintRegularNameFlag = SOC.Public.XML.SettingFile.ReadSetting(fileName, "Label", "ItemRegularName", "False");
                    return Neusoft.FrameWork.Function.NConvert.ToBoolean(labelPrintRegularNameFlag);
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
                    return Neusoft.FrameWork.Function.NConvert.ToBoolean(memoPrintFlag);
                }
                if (System.IO.File.Exists(fileName))
                {
                    memoPrintFlag = SOC.Public.XML.SettingFile.ReadSetting(fileName, "Label", "PrintMemo", "False");
                    return Neusoft.FrameWork.Function.NConvert.ToBoolean(memoPrintFlag);
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

        /// <summary>
        /// 获取诊断
        /// </summary>
        /// <param name="clincNO"></param>
        /// <returns></returns>
        private string GetDiagnose(string clinicNO)
        {
            ArrayList al = diagnoseMgr.QueryMainDiagnose(clinicNO, true, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                return "查询诊断出错：";
            }
            if (al.Count == 0)
            {
                al = diagnoseMgr.QueryMainDiagnose(clinicNO, false, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
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
                    Neusoft.HISFC.Models.HealthRecord.Diagnose diagnose = al[0] as Neusoft.HISFC.Models.HealthRecord.Diagnose;
                    return diagnose.DiagInfo.ICD10.Name;
                }
            }
            else
            {
                Neusoft.HISFC.Models.HealthRecord.Diagnose diagnose = al[0] as Neusoft.HISFC.Models.HealthRecord.Diagnose;
                return diagnose.DiagInfo.ICD10.Name;
            }
        }

        #region IOutpatientDrug 成员

        /// <summary>
        /// 保存后调用
        /// </summary>
        /// <param name="alData">出库申请实体applyout</param>
        /// <param name="drugRecipe">处方信息、患者信息</param>
        /// <param name="type">0是直接发药 1是配药 2是发药</param>
        /// <param name="drugTerminal">配药台或者发药窗</param>
        /// <returns></returns>
        public int AfterSave(System.Collections.ArrayList alData, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string type, Neusoft.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            return 0;
        }

        /// <summary>
        /// 自动打印时调用
        /// </summary>
        /// <param name="alData">出库申请实体applyout</param>
        /// <param name="drugRecipe">处方信息、患者信息</param>
        /// <param name="type">0是直接发药 1是配药 2是发药</param>
        /// <param name="drugTerminal">配药台或者发药窗</param>
        /// <returns></returns>
        public int OnAutoPrint(System.Collections.ArrayList alData, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string type, Neusoft.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            DateTime printTime = DateTime.Now;

            ArrayList alValid = this.GetValid(alData);

            if (alValid == null || alValid.Count == 0)
            {
                return -1;
            }

            if (drugTerminal.TerimalPrintType == Neusoft.HISFC.Models.Pharmacy.EnumClinicPrintType.标签)
            {
                this.myPrintDrugLabel(alValid, drugRecipe, type, drugTerminal);
            }
            else if (drugTerminal.TerimalPrintType == Neusoft.HISFC.Models.Pharmacy.EnumClinicPrintType.扩展)
            {
                ucDrugBill ucDrugBill = new ucDrugBill();
                ucDrugBill.PrintDrugBill(alData, drugRecipe, drugTerminal);

                ucInjectBill ucInjectBill = new ucInjectBill();
                ucInjectBill.PrintDrugBill(alData, drugRecipe, drugTerminal);

                this.myPrintDrugLabel(alValid, drugRecipe, type, drugTerminal);
            }
            else if (drugTerminal.TerimalPrintType == Neusoft.HISFC.Models.Pharmacy.EnumClinicPrintType.清单)
            {
                ucDrugBill ucDrugBill = new ucDrugBill();
                ucDrugBill.PrintDrugBill(alData, drugRecipe, drugTerminal);
            }
            return 0;
        }

        /// <summary>
        /// 打印清单调用，一般是补打时调用
        /// </summary>
        /// <param name="alData">出库申请实体applyout</param>
        /// <param name="drugRecipe">处方信息、患者信息</param>
        /// <param name="type">0是直接发药 1是配药 2是发药</param>
        /// <param name="drugTerminal">配药台或者发药窗</param>
        /// <returns></returns>
        public int PrintDrugBill(System.Collections.ArrayList alData, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, Neusoft.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            DateTime printTime = DateTime.Now;

            ArrayList alValid = this.GetValid(alData);
            if (alValid == null || alValid.Count == 0)
            {
                return -1;
            }

            ucInjectBill ucInjectBill = new ucInjectBill();
            ucInjectBill.PrintDrugBill(alData, drugRecipe, drugTerminal);

            return 0;
        }

        /// <summary>
        /// 打印标签调用，一般是补打时调用
        /// </summary>
        /// <param name="alData">出库申请实体applyout</param>
        /// <param name="drugRecipe">处方信息、患者信息</param>
        /// <param name="type">0是直接发药 1是配药 2是发药</param>
        /// <param name="drugTerminal">配药台或者发药窗</param>
        /// <returns></returns>
        public int PrintDrugLabel(System.Collections.ArrayList alData, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, Neusoft.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            ArrayList alValid = GetValid(alData);
            if (alValid == null || alValid.Count == 0)
            {
                return -1;
            }
            string type = "1";
            this.myPrintDrugLabel(alValid, drugRecipe, type, drugTerminal);
            return 0;
        }

        /// <summary>
        /// 打印处方调用，一般是补打时调用
        /// </summary>
        /// <param name="alData">出库申请实体applyout</param>
        /// <param name="drugRecipe">处方信息、患者信息</param>
        /// <param name="type">0是直接发药 1是配药 2是发药</param>
        /// <param name="drugTerminal">配药台或者发药窗</param>
        /// <returns></returns>
        public int PrintRecipe(System.Collections.ArrayList alData, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, Neusoft.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            DateTime printTime = DateTime.Now;

            ArrayList alVaid = this.GetValid(alData);

            if (alVaid == null || alVaid.Count == 0)
            {
                return -1;
            }

            ucDrugBill ucDrugBill = new ucDrugBill();
            ucDrugBill.PrintDrugBill(alData, drugRecipe, drugTerminal);

            return 0;
        }

        #endregion

        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugRecipe"></param>
        /// <param name="type"></param>
        /// <param name="drugTerminal"></param>
        /// <returns></returns>
        private int myPrintDrugLabel(System.Collections.ArrayList alData, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string type, Neusoft.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            for (int index = alData.Count - 1; index > -1; index--)
            {
                Neusoft.HISFC.Models.Pharmacy.ApplyOut applyOut = alData[index] as Neusoft.HISFC.Models.Pharmacy.ApplyOut;
                if (!SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(applyOut.Usage.ID))
                {
                    alData.RemoveAt(index);
                }
            }
            if (alData.Count == 0)
            {
                return 0;
            }
            DateTime printTime = DateTime.Now;
          
            ucDrugLabel ucDrugLabel = new ucDrugLabel();
            ucDrugLabel.PrintDrugLabel(alData, drugRecipe, drugTerminal, IsLabelPrintRegularName, HospitalName, IsPrintMemo, GetDiagnose(drugRecipe.ClinicNO), printTime);
            return 0;
        }

        private ArrayList GetValid(ArrayList alData)
        {
            if (alData == null || alData.Count == 0)
            {
                return new ArrayList();
            }
            ArrayList alValid = new ArrayList();
            foreach (Neusoft.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                if (applyOut.ValidState == Neusoft.HISFC.Models.Base.EnumValidState.Valid)
                {
                    alValid.Add(applyOut);
                }
            }
            return alValid;
        }
    }
}
