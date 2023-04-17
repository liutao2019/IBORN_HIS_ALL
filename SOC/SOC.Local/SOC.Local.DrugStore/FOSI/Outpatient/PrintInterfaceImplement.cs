using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.DrugStore.FOSI.Outpatient
{

    /// <summary>
    /// [功能描述: 门诊药房打印本地化实例]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// 1、主要是实现接口函数逻辑
    /// 2、不要相信一个处方只有5个药品，任何情况都考虑分页的可能
    /// 3、不要相信一个终端只有一种药品类别，任何终端都有可能有两种或者两种以上类别
    /// 4、不要相信一个终端只有一种打印方式，考虑药品类别对应打印方式是比较合理的
    /// 5、任何情况下都考虑有作废的数据
    /// </summary>>
    public class PrintInterfaceImplement : FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientDrug
    {
        private string fileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugStorePrintSetting.xml";

        FS.HISFC.BizLogic.HealthRecord.Diagnose diagnoseMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

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

        /// <summary>
        /// 获取诊断
        /// </summary>
        /// <param name="clincNO"></param>
        /// <returns></returns>
        private string GetDiagnose(string clinicNO)
        {
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagnoseMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            ArrayList al = diagnoseMgr.QueryMainDiagnose(clinicNO, true, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                return "查询诊断出错：";
            }
            if (al.Count == 0)
            {
                al = diagnoseMgr.QueryCaseDiagnoseForClinicByState(clinicNO, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, true);
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

        #region IOutpatientDrug 成员

        /// <summary>
        /// 保存后调用
        /// </summary>
        /// <param name="alData">出库申请实体applyout</param>
        /// <param name="drugRecipe">处方信息、患者信息</param>
        /// <param name="type">0是直接发药 1是配药 2是发药</param>
        /// <param name="drugTerminal">配药台或者发药窗</param>
        /// <returns></returns>
        public int AfterSave(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string type, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            DateTime printTime = DateTime.Now;

            ArrayList alVaid = this.GetValid(alData);

            if (alVaid == null || alVaid.Count == 0)
            {
                return -1;
            }

            if (type == "2" || type == "0")
            {                
                string operName = FS.FrameWork.Management.Connection.Operator.Name;
                if (string.IsNullOrEmpty(FS.FrameWork.Management.Connection.Operator.Name))
                {
                    operName = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(FS.FrameWork.Management.Connection.Operator.ID);
                }
                string operDeptName = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.Name;
                if (string.IsNullOrEmpty(operDeptName))
                {
                    operDeptName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
                }
                WorkAppriaseExternInterfaceImplement.WorkAppriase(operName, operDeptName, drugRecipe.CardNO, drugRecipe.PatientName);
            }
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
        public int OnAutoPrint(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string type, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            DateTime printTime = DateTime.Now;

            //打印退单，不区分有效、无效的
            //ArrayList alValid = this.GetValid(alData);
            ArrayList alValid = alData;
            if (alValid == null || alValid.Count == 0)
            {
                return -1;
            }
            //退药，用于打印标签头
            ArrayList alTY = new ArrayList();

            //南庄项目
            if (drugTerminal.TerimalPrintType == FS.HISFC.Models.Pharmacy.EnumClinicPrintType.标签)
            {
                //草药
                ArrayList alPCC = new ArrayList();

                //西药和中成药
                ArrayList alPZ = new ArrayList();

                //严格来讲分方不可以将中成药、西药和草药分在一起，打印清单时这个肯定是分开的
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alValid)
                {
                    //标签暂时不打印无效的
                    if (applyOut.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                    {
                        if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSysClass(applyOut.Item.ID) != "PCC")
                        {
                            alTY.Add(applyOut);
                        }
                        continue;
                    }
                    if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSysClass(applyOut.Item.ID) == "PCC")
                    {
                        alPCC.Add(applyOut);
                    }
                    else
                    {
                        alPZ.Add(applyOut);
                    }
                }

                //草药按照草药清单打印
                if (alPCC.Count > 0)
                {
                    FOSI.Outpatient.ucHerbalDrugList ucHerbalDrugList = new ucHerbalDrugList();
                    ucHerbalDrugList.Print(alPCC, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName, printTime);
                }
                if (alPZ.Count > 0)
                {
                    this.myPrintDrugLabel(alPZ, drugRecipe, type, drugTerminal);
                }
            }
            else if (drugTerminal.TerimalPrintType == FS.HISFC.Models.Pharmacy.EnumClinicPrintType.扩展)
            {
                //草药
                ArrayList alPCC = new ArrayList();

                //西药和中成药
                ArrayList alPZ = new ArrayList();

                //严格来讲分方不可以将中成药、西药和草药分在一起，打印清单时这个肯定是分开的
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alValid)
                {
                    if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSysClass(applyOut.Item.ID) == "PCC")
                    {
                        if (applyOut.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                        {
                            continue;
                        }
                        alPCC.Add(applyOut);
                    }
                    else
                    {
                        alPZ.Add(applyOut);
                    }
                }

                //草药按照草药清单打印
                if (alPCC.Count > 0)
                {
                    FOSI.Outpatient.ucHerbalDrugList ucHerbalDrugList = new ucHerbalDrugList();
                    ucHerbalDrugList.Print(alPCC, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName, printTime);
                }
                if (alPZ.Count > 0)
                {
                    //西药、中成药的清单格式
                    FOSI.Outpatient.ucDrugList ucDrugList = new ucDrugList();
                    //南庄口服和针剂分开
                    ArrayList alPO = new ArrayList();
                    ArrayList alZJ = new ArrayList();
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alPZ)
                    {
                        if (SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(applyOut.Usage.ID))
                        {
                            alZJ.Add(applyOut);
                        }
                        else
                        {
                            alPO.Add(applyOut);
                        }
                    }
                    if (alPO.Count > 0)
                    {
                        ucDrugList.PrintDrugBill(alPO, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName);
                    }
                    if (alZJ.Count > 0)
                    {
                        ucDrugList.PrintDrugBill(alZJ, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName);
                    }
                }
            }
            else if (drugTerminal.TerimalPrintType == FS.HISFC.Models.Pharmacy.EnumClinicPrintType.清单)
            {
                //草药
                ArrayList alPCC = new ArrayList();

                //西药和中成药
                ArrayList alPZ = new ArrayList();

                //严格来讲分方不可以将中成药、西药和草药分在一起，打印清单时这个肯定是分开的
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alValid)
                {
                    if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSysClass(applyOut.Item.ID) == "PCC")
                    {
                        if (applyOut.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                        {
                            continue;
                        }
                        alPCC.Add(applyOut);
                    }
                    else
                    {
                        alPZ.Add(applyOut);
                    }
                }

                if (alPCC.Count > 0)
                {
                    FOSI.Outpatient.ucHerbalDrugList ucHerbalDrugList = new ucHerbalDrugList();
                    ucHerbalDrugList.Print(alPCC, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName, printTime);
                }

                if (alPZ.Count > 0)
                {
                    //西药、中成药的清单格式
                    FOSI.Outpatient.ucFoSiDrugList ucDrugList = new ucFoSiDrugList();
                    //南庄口服和针剂分开
                    ArrayList alPO = new ArrayList();
                    ArrayList alZJ = new ArrayList();
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alPZ)
                    {
                        if (SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(applyOut.Usage.ID))
                        {
                            alZJ.Add(applyOut);
                        }
                        else
                        {
                            alPO.Add(applyOut);
                        }
                    }
                    if (alPO.Count > 0)
                    {
                        ucDrugList.PrintDrugBill(alPO, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName);
                    }
                    if (alZJ.Count > 0)
                    {
                        ucDrugList.PrintDrugBill(alZJ, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName);
                    }
                }
            }
            //标签打印要打印一个标签头
            if (alTY.Count > 0)
            {
                this.myPrintDrugLabelByTY(alTY, drugRecipe, type, drugTerminal);
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
        public int PrintDrugBill(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            DateTime printTime = DateTime.Now;

            //ArrayList alValid = this.GetValid(alData);
            ArrayList alValid = alData;
            if (alValid == null || alValid.Count == 0)
            {
                return -1;
            }


            //草药
            ArrayList alPCC = new ArrayList();

            //西药和中成药
            ArrayList alPZ = new ArrayList();

            //严格来讲分方不可以将中成药、西药和草药分在一起，打印清单时这个肯定是分开的
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alValid)
            {
                if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSysClass(applyOut.Item.ID) == "PCC")
                {
                    if (applyOut.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                    {
                        continue;
                    }
                    alPCC.Add(applyOut);
                }
                else
                {
                    alPZ.Add(applyOut);
                }
            }

            if (alPCC.Count > 0)
            {
                FOSI.Outpatient.ucHerbalDrugList ucHerbalDrugList = new ucHerbalDrugList();
                ucHerbalDrugList.Print(alPCC, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName, printTime);
            }

            if (alPZ.Count > 0)
            {
                //西药、中成药的清单格式
                FOSI.Outpatient.ucDrugList ucDrugList = new ucDrugList();
                //南庄口服和针剂分开
                ArrayList alPO = new ArrayList();
                ArrayList alZJ = new ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alPZ)
                {
                    if (SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(applyOut.Usage.ID))
                    {
                        alZJ.Add(applyOut);
                    }
                    else
                    {
                        alPO.Add(applyOut);
                    }
                }
                if (alPO.Count > 0)
                {
                    ucDrugList.PrintDrugBill(alPO, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName);
                }
                if (alZJ.Count > 0)
                {
                    ucDrugList.PrintDrugBill(alZJ, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName);
                }
            }

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
        public int PrintDrugLabel(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
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
        public int PrintRecipe(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            DateTime printTime = DateTime.Now;

            ArrayList alVaid = this.GetValid(alData);

            if (alVaid == null || alVaid.Count == 0)
            {
                return -1;
            }
            return 0;
        }

        #endregion

        private int myPrintDrugLabel(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string type, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            DateTime printTime = DateTime.Now;

           
            //南庄项目打印标签
            FOSI.Outpatient.ucReicpeInfoLabel ucReicpeInfoLabel = new FOSI.Outpatient.ucReicpeInfoLabel();
            ucReicpeInfoLabel.PrintDrugLabel(alData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, this.GetDiagnose(drugRecipe.ClinicNO), printTime,false);

            //分类组合药品
            Hashtable hsCombo = new Hashtable();
            //记录顺序
            ArrayList alSort = new ArrayList();
            //总页数
            int totPageNO = 0;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                if (hsCombo.Contains(applyOut.CombNO))
                {
                    ArrayList alCombo = hsCombo[applyOut.CombNO] as ArrayList;
                    if (alCombo.Count == 4)
                    {
                        hsCombo.Clear();
                        alSort.Clear();
                        break;
                    }
                    alCombo.Add(applyOut);
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(applyOut);
                    hsCombo.Add(applyOut.CombNO, al);
                    alSort.Add(applyOut.CombNO);
                    totPageNO++;
                }
            }
            int curPageNO = 0;
            if (hsCombo.Values.Count == 0)
            {
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
                {
                    curPageNO++;
                    ArrayList alPrintData = new ArrayList();
                    alPrintData.Add(applyOut);
                    FOSI.Outpatient.ucDrugLabel ucDrugLabel = new FOSI.Outpatient.ucDrugLabel();
                    ucDrugLabel.PrintDrugLabel(alPrintData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, QtyShowType, printTime, curPageNO.ToString() + "/" + totPageNO.ToString());
                }
            }
            foreach (string comboNO in alSort)
            {
                curPageNO++;

                ArrayList alPrintData = hsCombo[comboNO] as ArrayList;

                if (alPrintData.Count == 1)
                {
                    FOSI.Outpatient.ucDrugLabel ucDrugLabel = new FOSI.Outpatient.ucDrugLabel();
                    ucDrugLabel.PrintDrugLabel(alPrintData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, QtyShowType, printTime, curPageNO.ToString() + "/" + totPageNO.ToString());

                }
                else if (alPrintData.Count == 2)
                {
                    FOSI.Outpatient.ucComboDrugLabel ucDrugLabel = new FOSI.Outpatient.ucComboDrugLabel();
                    ucDrugLabel.PrintDrugLabel(alPrintData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, QtyShowType, printTime, curPageNO.ToString() + "/" + totPageNO.ToString());

                }
                else if (alPrintData.Count == 3)
                {
                    FOSI.Outpatient.ucComboDrugLabel1 ucDrugLabel = new FOSI.Outpatient.ucComboDrugLabel1();
                    ucDrugLabel.PrintDrugLabel(alPrintData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, QtyShowType, printTime, curPageNO.ToString() + "/" + totPageNO.ToString());

                }
                else if (alPrintData.Count == 4)
                {
                    FOSI.Outpatient.ucComboDrugLabel2 ucDrugLabel = new FOSI.Outpatient.ucComboDrugLabel2();
                    ucDrugLabel.PrintDrugLabel(alPrintData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, QtyShowType, printTime, curPageNO.ToString() + "/" + totPageNO.ToString());

                }
            }

            return 1;
        }
        private int myPrintDrugLabelByTY(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string type, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            DateTime printTime = DateTime.Now;
            FOSI.Outpatient.ucReicpeInfoLabel ucReicpeInfoLabel = new FOSI.Outpatient.ucReicpeInfoLabel();
            ucReicpeInfoLabel.PrintDrugLabel(alData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, this.GetDiagnose(drugRecipe.ClinicNO), printTime,true);
            return 1;
        }
        private ArrayList GetValid(ArrayList alData)
        {
            if (alData == null || alData.Count == 0)
            {
                return new ArrayList();
            }
            ArrayList alValid = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    alValid.Add(applyOut);
                }
            }
            return alValid;
        }

        #region IOutpatientDrug 成员


        public int PrintDrugBag(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            return 0;
           
        }

        #endregion
    }
}
