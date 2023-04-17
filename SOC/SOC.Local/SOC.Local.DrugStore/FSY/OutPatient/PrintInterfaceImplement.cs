using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.DrugStore.FSY.Outpatient
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

        FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();

        string labelPrintRegularNameFlag = "-1";

        //是否汇总打印
        bool isPrintByTotal= true;

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

        private int GetMaxRecipe(ArrayList allData)
        {
            int maxRecipeNo = 0;
            if (allData.Count == 0 || allData == null)
            {
                return maxRecipeNo;
            }
            else
            {
                for (int i = 0; i < allData.Count; i++)
                {
                    FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe = allData[i] as FS.HISFC.Models.Pharmacy.DrugRecipe;
                    if (maxRecipeNo <= FS.FrameWork.Function.NConvert.ToInt32(drugRecipe.RecipeNO))
                    {
                        maxRecipeNo =  FS.FrameWork.Function.NConvert.ToInt32(drugRecipe.RecipeNO);
                    }
                }
                return maxRecipeNo;
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
            ArrayList alValid = this.GetValid(alData);
            //ArrayList alValid = alData;
            if (alValid == null || alValid.Count == 0)
            {
                return -1;
            }            

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
                    FSY.Outpatient.ucHerbalDrugList ucHerbalDrugList = new ucHerbalDrugList();
                    ucHerbalDrugList.Print(alPCC, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName, printTime);
                }
                if (alPZ.Count > 0)
                {
                    this.myPrintDrugLabel(alPZ, drugRecipe, type, drugTerminal);
                }
            }
            else if (drugTerminal.TerimalPrintType == FS.HISFC.Models.Pharmacy.EnumClinicPrintType.扩展)
            {
                return 0;
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
                    FSY.Outpatient.ucHerbalDrugList ucHerbalDrugList = new ucHerbalDrugList();
                    ucHerbalDrugList.Print(alPCC, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName, printTime);
                }
                if (alPZ.Count > 0)
                {

                    this.myPrintDrugLabel(alPZ, drugRecipe, type, drugTerminal);
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
                    FSY.Outpatient.ucHerbalDrugList ucHerbalDrugList = new ucHerbalDrugList();
                    ucHerbalDrugList.Print(alPCC, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName, printTime);
                }

                if (alPZ.Count > 0)
                {
                    //西药、中成药的清单格式
                    FSY.Outpatient.ucFSYDrugList ucDrugList = new ucFSYDrugList();
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
                FSY.Outpatient.ucHerbalDrugList ucHerbalDrugList = new ucHerbalDrugList();
                ucHerbalDrugList.Print(alPCC, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName, printTime);
            }

            if (alPZ.Count > 0)
            {
                //西药、中成药的清单格式
                FSY.Outpatient.ucDrugList ucDrugList = new ucDrugList();
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
            if (drugTerminal.TerimalPrintType == FS.HISFC.Models.Pharmacy.EnumClinicPrintType.扩展)
            {
                DateTime printTime = DateTime.Now;



                //佛三项目打印标签
                FSY.Outpatient.ucReicpeInfoLabelZJ ucReicpeInfoLabelZJ = new FSY.Outpatient.ucReicpeInfoLabelZJ();
                ucReicpeInfoLabelZJ.PrintDrugLabel(alData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, this.GetDiagnose(drugRecipe.ClinicNO), printTime);
              


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
                        FSY.Outpatient.ucDrugLabelZJ ucDrugLabelZJ = new FSY.Outpatient.ucDrugLabelZJ();
                        ucDrugLabelZJ.PrintDrugLabel(alPrintData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, QtyShowType, printTime, curPageNO.ToString() + "/" + totPageNO.ToString());
                    }
                }
                foreach (string comboNO in alSort)
                {
                    curPageNO++;

                    ArrayList alPrintData = hsCombo[comboNO] as ArrayList;

                    if (alPrintData.Count == 1)
                    {
                        FSY.Outpatient.ucDrugLabelZJ ucDrugLabelZJ = new FSY.Outpatient.ucDrugLabelZJ();
                        ucDrugLabelZJ.PrintDrugLabel(alPrintData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, QtyShowType, printTime, curPageNO.ToString() + "/" + totPageNO.ToString());

                    }
                    else if (alPrintData.Count == 2)
                    {
                        FSY.Outpatient.ucComboDrugLabel ucDrugLabel = new FSY.Outpatient.ucComboDrugLabel();
                        ucDrugLabel.PrintDrugLabel(alPrintData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, QtyShowType, printTime, curPageNO.ToString() + "/" + totPageNO.ToString());

                    }
                    else if (alPrintData.Count == 3)
                    {
                        FSY.Outpatient.ucComboDrugLabel1 ucDrugLabel = new FSY.Outpatient.ucComboDrugLabel1();
                        ucDrugLabel.PrintDrugLabel(alPrintData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, QtyShowType, printTime, curPageNO.ToString() + "/" + totPageNO.ToString());

                    }
                    else if (alPrintData.Count == 4)
                    {
                        FSY.Outpatient.ucComboDrugLabel2 ucDrugLabel = new FSY.Outpatient.ucComboDrugLabel2();
                        ucDrugLabel.PrintDrugLabel(alPrintData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, QtyShowType, printTime, curPageNO.ToString() + "/" + totPageNO.ToString());

                    }
                }
            }
            else
            {

                DateTime printTime = DateTime.Now;
                //临时卡号不打印
                if (drugRecipe.CardNO.StartsWith("9"))
                {
                    return -1;
                }
                ArrayList allInvoiceData = new ArrayList();
                FS.HISFC.Models.Pharmacy.ApplyOut applyInfo = alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                //根据发票号查询该库存科室所有药品项目
                allInvoiceData = drugStoreMgr.QueryApplyOutListForInvoiceNO(drugRecipe.StockDept.ID, "M1", applyInfo.State, drugRecipe.InvoiceNO);

                if (allInvoiceData.Count == 0 || allInvoiceData == null)
                {
                    return -1;
                }

                //对于未打印的处方如果按发票查出的数据已经存在打印了的直接返回
                if (drugRecipe.RecipeState == "0")
                {
                    ArrayList allRecipe = this.drugStoreMgr.QueryDrugRecipeByInvoice(applyInfo.StockDept.ID, "M1", drugRecipe.InvoiceNO);
                    int recipeNO = this.GetMaxRecipe(allRecipe);
                    if (drugRecipe.RecipeNO != recipeNO.ToString())
                    {
                        return -1;
                    }
                }

                //增加用法不为空的项目 排除手工方
                ArrayList allUsageData = new ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in allInvoiceData)
                {
                    if (!string.IsNullOrEmpty(applyOut.Usage.ID))
                    {
                        allUsageData.Add(applyOut);
                    }
                }

                if (allUsageData.Count > 0 && allUsageData != null)
                {
                    //南庄项目打印标签
                    FSY.Outpatient.ucReicpeInfoLabel ucReicpeInfoLabel = new FSY.Outpatient.ucReicpeInfoLabel();
                    ucReicpeInfoLabel.PrintDrugLabel(allUsageData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, this.GetDiagnose(drugRecipe.ClinicNO), printTime);
                }
                else
                {
                    return -1;
                }

                //分类组合药品
                Hashtable hsCombo = new Hashtable();
                //记录顺序
                ArrayList alSort = new ArrayList();
                //总页数
                int totPageNO = 0;
                int curPageNO = 0;
                if (!isPrintByTotal)
                {
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in allUsageData)
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
                }
                else
                {
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in allUsageData)
                    {
                        string keys = applyOut.Item.ID + applyOut.Usage.ID + applyOut.Frequency.ID + applyOut.DoseOnce + SOC.Local.DrugStore.Common.Function.GetOrder(applyOut.OrderNO).Memo;;
                        if (hsCombo.Contains(keys))
                        {
                            ArrayList alCombo = hsCombo[keys] as ArrayList;
                            alCombo.Add(applyOut);
                        }
                        else
                        {
                            ArrayList al = new ArrayList();
                            al.Add(applyOut);
                            hsCombo.Add(keys, al);
                            alSort.Add(keys);
                            totPageNO++;
                        }
                    }
                }
                if (hsCombo.Values.Count == 0)
                {
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in

allUsageData)
                    {
                        curPageNO++;
                        ArrayList alPrintData = new ArrayList();
                        alPrintData.Add(applyOut);
                        FSY.Outpatient.ucDrugLabel ucDrugLabel = new

FSY.Outpatient.ucDrugLabel();
                        ucDrugLabel.PrintDrugLabel(alPrintData, drugRecipe,

drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, QtyShowType,

printTime, curPageNO.ToString() + "/" + totPageNO.ToString());
                    }
                }
                foreach (string comboNO in alSort)
                {
                    curPageNO++;
                    if (!isPrintByTotal)
                    {
                        ArrayList alPrintData = hsCombo[comboNO] as ArrayList;
                        if (alPrintData.Count == 1)
                        {
                            FSY.Outpatient.ucDrugLabel ucDrugLabel = new FSY.Outpatient.ucDrugLabel();
                            ucDrugLabel.PrintDrugLabel(alPrintData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, QtyShowType, printTime, curPageNO.ToString() + "/" + totPageNO.ToString());
                        }
                        else
                        {
                            for (int i = 0; i < alPrintData.Count; i++)
                            {
                                ArrayList printData = new ArrayList();
                                printData.Add(alPrintData[i]);
                                FSY.Outpatient.ucDrugLabel ucDrugLabel = new FSY.Outpatient.ucDrugLabel();
                                ucDrugLabel.PrintDrugLabel(printData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, QtyShowType, printTime, curPageNO.ToString() + "/" + totPageNO.ToString());
                            }
                        }
                    }
                    else
                    {
                        ArrayList alPrintData = hsCombo[comboNO] as ArrayList;
                        decimal sumQTY = 0;
                        FS.HISFC.Models.Pharmacy.ApplyOut appInfo = alPrintData[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alPrintData)
                        {
                            sumQTY += applyOut.Operation.ApplyQty; ;
                        }
                        appInfo.Operation.ApplyQty = sumQTY;
                        ArrayList allPrintData = new ArrayList();
                        allPrintData.Add(appInfo);

                        if (allPrintData.Count == 1)
                        {
                            FSY.Outpatient.ucDrugLabel ucDrugLabel = new FSY.Outpatient.ucDrugLabel();
                            ucDrugLabel.PrintDrugLabel(allPrintData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, QtyShowType, printTime, curPageNO.ToString() + "/" + totPageNO.ToString());
                        }
                        else
                        {
                            for (int i = 0; i < alPrintData.Count; i++)
                            {
                                ArrayList printData = new ArrayList();
                                printData.Add(alPrintData[i]);
                                FSY.Outpatient.ucDrugLabel ucDrugLabel = new FSY.Outpatient.ucDrugLabel();
                                ucDrugLabel.PrintDrugLabel(printData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, QtyShowType, printTime, curPageNO.ToString() + "/" + totPageNO.ToString());
                            }
                        }
                    }
                }
 
            }

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
