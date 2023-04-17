using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GJLocal.HISFC.Components.OpGuide.DrugStore
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

        FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();

        FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
            
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
                    labelPrintRegularNameFlag = FS.SOC.Public.XML.SettingFile.ReadSetting(fileName, "Label", "ItemRegularName", "False");
                    return FS.FrameWork.Function.NConvert.ToBoolean(labelPrintRegularNameFlag);
                }
                else
                {
                    labelPrintRegularNameFlag = "False";
                    FS.SOC.Public.XML.SettingFile.SaveSetting(fileName, "Label", "ItemRegularName", "False");
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
                    hospitalName = FS.SOC.Public.XML.SettingFile.ReadSetting(fileName, "Hospital", "Name", "");
                }
                else
                {
                    FS.SOC.Public.XML.SettingFile.SaveSetting(fileName, "Hospital", "Name", "");
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
                    memoPrintFlag = FS.SOC.Public.XML.SettingFile.ReadSetting(fileName, "Label", "PrintMemo", "False");
                    return FS.FrameWork.Function.NConvert.ToBoolean(memoPrintFlag);
                }
                else
                {
                    memoPrintFlag = "False";
                    FS.SOC.Public.XML.SettingFile.SaveSetting(fileName, "Label", "PrintMemo", "False");
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
                    return FS.SOC.Public.XML.SettingFile.ReadSetting(fileName, "Label", "QtyShowType", "包装单位");
                }
                else
                {
                    FS.SOC.Public.XML.SettingFile.SaveSetting(fileName, "Label", "QtyShowType", "包装单位");
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
                string diag = string.Empty;
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.HealthRecord.Diagnose diagnose = al[i] as FS.HISFC.Models.HealthRecord.Diagnose;
                    if (diagnose.IsValid)
                    {
                        diag = diagnose.DiagInfo.ICD10.Name;
                        break;
                    }                    
                }
                return diag;
                
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
            //{15608628-B4F6-44c0-8034-E1297A4AA402}
            DateTime printTime = DateTime.Now;

            ArrayList alValid = this.GetValid(alData);

            if (alValid == null || alValid.Count == 0)
            {
                return -1;
            }
            
            FS.HISFC.Models.Base.Employee curEmployee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department curDepartment = curEmployee.Dept as FS.HISFC.Models.Base.Department;

            //if (curDepartment.HospitalID == "BELLAIRE")
            if (curDepartment.HospitalID == "BELLAIRE" )
            {
                #region 贝利尔
                string langue = "1";
                FS.HISFC.Models.Registration.Register regInfo = this.regMgr.GetByClinic(drugRecipe.ClinicNO);
                string InfoStr = regInfo.PID.CardNO + " " + regInfo.Name + "[" + regInfo.Sex.Name + "]";
                GJLocal.HISFC.Components.OpGuide.DrugStore.frmChooseDrugLableLangue frmLangueChoose = new frmChooseDrugLableLangue();
                frmLangueChoose.SetInfo(InfoStr);
                frmLangueChoose.ShowDialog();

                if (frmLangueChoose.Langue == "2")
                {

                    //草药
                    ArrayList alPCC = new ArrayList();

                    //口服药
                    ArrayList alPO = new ArrayList();

                    //外用药
                    ArrayList alUnPO = new ArrayList();
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alValid)
                    {
                        if (FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSysClass(applyOut.Item.ID) == "PCC")
                        {
                            alPCC.Add(applyOut);
                        }
                        else
                        {

                            if (FS.SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(applyOut.Usage.ID))
                            {
                                alPO.Add(applyOut);
                            }
                            else
                            {
                                alUnPO.Add(applyOut);
                            }
                        }
                    }

                    if (alPCC.Count > 0)
                    {
                        GJLocal.HISFC.Components.OpGuide.DrugStore.IBORN.ucRecipeInfoLabelHerbalIBORN ucRecipeInfoLabelHerbalIBORN = new GJLocal.HISFC.Components.OpGuide.DrugStore.IBORN.ucRecipeInfoLabelHerbalIBORN();
                        ucRecipeInfoLabelHerbalIBORN.PrintDrugLabel(alData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, curDepartment.HospitalName, IsPrintMemo, this.GetDiagnose(drugRecipe.ClinicNO), printTime);
                    }

                    if (alPO.Count > 0)
                    {
                        DateTime printTime2 = DateTime.Now;
                        GJLocal.HISFC.Components.OpGuide.DrugStore.BLE.ucReicpeInfoLabelJPoBLE ucReicpeInfoLabelJPo = new GJLocal.HISFC.Components.OpGuide.DrugStore.BLE.ucReicpeInfoLabelJPoBLE();
                        ucReicpeInfoLabelJPo.PrintDrugLabel(alPO, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, "", printTime2);
                    }

                    if (alUnPO.Count > 0)
                    {
                        DateTime printTime3 = DateTime.Now;
                        GJLocal.HISFC.Components.OpGuide.DrugStore.BLE.ucReicpeInfoLabelJOutUseBLE ucReicpeInfoLabelJOutUse = new GJLocal.HISFC.Components.OpGuide.DrugStore.BLE.ucReicpeInfoLabelJOutUseBLE();
                        ucReicpeInfoLabelJOutUse.PrintDrugLabel(alUnPO, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, curDepartment.HospitalName, IsPrintMemo, "", printTime3);
                    }
                }
                else
                {

                    //草药
                    ArrayList alPCC = new ArrayList();

                    //非草药
                    ArrayList alOTH = new ArrayList();

                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alValid)
                    {
                        if (FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSysClass(applyOut.Item.ID) == "PCC")
                        {
                            alPCC.Add(applyOut);
                        }
                        else
                        {
                            alOTH.Add(applyOut);
                        }
                    }

                    if (alPCC.Count > 0)
                    {
                        GJLocal.HISFC.Components.OpGuide.DrugStore.IBORN.ucRecipeInfoLabelHerbalIBORN ucRecipeInfoLabelHerbalIBORN = new GJLocal.HISFC.Components.OpGuide.DrugStore.IBORN.ucRecipeInfoLabelHerbalIBORN();
                        ucRecipeInfoLabelHerbalIBORN.PrintDrugLabel(alData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, this.GetDiagnose(drugRecipe.ClinicNO), printTime);
                    }

                    if (alOTH.Count > 0)
                    {

                        DateTime printTime4 = DateTime.Now;
                        GJLocal.HISFC.Components.OpGuide.DrugStore.BLE.ucReicpeInfoLabelEBLE ucReicpeInfoLabelE = new GJLocal.HISFC.Components.OpGuide.DrugStore.BLE.ucReicpeInfoLabelEBLE();// {7DBB85BF-547C-4230-8598-55A2A4AD83F4}
                        ucReicpeInfoLabelE.PrintDrugLabel(alOTH, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, "", printTime4);
                    }
                }
                #endregion
            }
            else
            {
                //{E2514E38-1A31-4571-B3A3-796BD4252FA2}
                #region 爱博恩
                // 爱博恩
                if (drugTerminal.TerimalPrintType == FS.HISFC.Models.Pharmacy.EnumClinicPrintType.标签)
                {
                    //草药
                    ArrayList alPCC = new ArrayList();

                    //西药和中成药
                    ArrayList alPZ = new ArrayList();

                    //严格来讲分方不可以将中成药、西药和草药分在一起，打印清单时这个肯定是分开的
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alValid)
                    {
                        if (FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSysClass(applyOut.Item.ID) == "PCC")
                        {
                            alPCC.Add(applyOut);
                        }
                        else
                        {
                            alPZ.Add(applyOut);
                        }
                    }

                    //{C75B6782-A721-4de4-B41B-58A4AD92F3E5}
                    //草药不打印标签
                    if (alPCC.Count > 0)
                    {
                        //GJLocal.HISFC.Components.OpGuide.DrugStore.ucReicpeInfoLabelPCC ucRecipeDrugListPCC = new ucReicpeInfoLabelPCC();
                        //ucRecipeDrugListPCC.PrintDrugLabel(alData, drugRecipe, drugTerminal, false, string.Empty, false, string.Empty, DateTime.MinValue);
                        //GJLocal.HISFC.Components.OpGuide.DrugStore.ucHerbalDrugList ucHerbalDrugList = new ucHerbalDrugList();
                        //ucHerbalDrugList.Print(alPCC, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName, printTime, 1);

                        GJLocal.HISFC.Components.OpGuide.DrugStore.IBORN.ucRecipeInfoLabelHerbalIBORN ucRecipeInfoLabelHerbalIBORN = new GJLocal.HISFC.Components.OpGuide.DrugStore.IBORN.ucRecipeInfoLabelHerbalIBORN();
                        ucRecipeInfoLabelHerbalIBORN.PrintDrugLabel(alData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, curDepartment.HospitalName, IsPrintMemo, this.GetDiagnose(drugRecipe.ClinicNO), printTime);
                    
                    }

                    if (alPZ.Count > 0)
                    {
                        this.myPrintDrugLabel(alValid, drugRecipe, type, drugTerminal);
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
                        if (FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSysClass(applyOut.Item.ID) == "PCC")
                        {
                            alPCC.Add(applyOut);
                        }
                        else
                        {
                            alPZ.Add(applyOut);
                        }
                    }

                    if (alPCC.Count > 0)
                    {
                        //{C5D52603-39BF-4e2d-9732-6FEF5BD4BEA7}
                        //GJLocal.HISFC.Components.OpGuide.DrugStore.ucReicpeInfoLabelPCC ucRecipeDrugListPCC = new ucReicpeInfoLabelPCC();
                        //ucRecipeDrugListPCC.PrintDrugLabel(alData, drugRecipe, drugTerminal, false, string.Empty, false, string.Empty, DateTime.MinValue);
                        GJLocal.HISFC.Components.OpGuide.DrugStore.ucHerbalDrugList ucHerbalDrugList = new ucHerbalDrugList();
                        ucHerbalDrugList.Print(alPCC, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName, printTime, 1);
                    }

                    if (alPZ.Count > 0)
                    {
                        //西药、中成药的清单格式
                        GJLocal.HISFC.Components.OpGuide.DrugStore.ucHerbalDrugList ucHerbalDrugList = new ucHerbalDrugList();
                        ucHerbalDrugList.Print(alPZ, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, hospitalName, printTime, 1);
                    }
                }
                #endregion
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
            
            //this.myPrintDrugLabel(alValid, drugRecipe, type, drugTerminal);

            //{15608628-B4F6-44c0-8034-E1297A4AA402}
            this.OnAutoPrint(alData, drugRecipe, type, drugTerminal);

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
            // FS.HISFC.Models.Base.Employee curEmployee = FS.FrameWork.Management.Connection.ApplyOperator as FS.HISFC.Models.Base.Employee;
            //FS.HISFC.Models.Base.Department curDepartment = curEmployee.Dept as FS.HISFC.Models.Base.Department;
            FS.HISFC.Models.Base.Employee curEmployee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department curDepartment = curEmployee.Dept as FS.HISFC.Models.Base.Department;

            //if (curDepartment.HospitalID == "BELLAIRE")
            if(curDepartment.HospitalID.Contains("IBORN"))
            {
                //{E2514E38-1A31-4571-B3A3-796BD4252FA2}
                #region 爱博恩
                DateTime printTime = DateTime.Now;

                //
                GJLocal.HISFC.Components.OpGuide.DrugStore.IBORN.ucReicpeInfoLabelIBORN ucReicpeInfoLabel = new GJLocal.HISFC.Components.OpGuide.DrugStore.IBORN.ucReicpeInfoLabelIBORN();
                ucReicpeInfoLabel.PrintDrugLabel(alData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, this.GetDiagnose(drugRecipe.ClinicNO), printTime);
                #endregion
            }
            else
            //if (curDepartment.HospitalID != "IBORN")
            {
                //{E2514E38-1A31-4571-B3A3-796BD4252FA2}
                #region 贝利尔
                DateTime printTime = DateTime.Now;
                //{5980AEF4-5140-41ab-8064-D17B6EB6F4A7}
                FS.HISFC.Models.Registration.Register regInfo = this.regMgr.GetByClinic(drugRecipe.ClinicNO);
                string InfoStr = regInfo.PID.CardNO + " " + regInfo.Name + "[" + regInfo.Sex.Name + "]";
                GJLocal.HISFC.Components.OpGuide.DrugStore.frmChooseDrugLableLangue frmLangueChoose = new frmChooseDrugLableLangue();
                frmLangueChoose.SetInfo(InfoStr);
                frmLangueChoose.ShowDialog();
                if (frmLangueChoose.IsCanContine())
                {
                    //{4B8A931E-FD8D-4786-BBA3-459D2A9B4534} lfhm
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
                    {
                        if (applyOut.PrintState == "0" || string.IsNullOrEmpty(applyOut.PrintState))
                        {
                            string applyNum = applyOut.ID;
                            string operID = FS.FrameWork.Management.Connection.Operator.ID;
                            if (drugStoreManager.UpdateAppleOutPrintState("1", "1", operID, applyNum) == -1)
                            {
                                return -1;
                            }
                        }
                    }
                    //{4B8A931E-FD8D-4786-BBA3-459D2A9B4534} lfhm
                    if (drugRecipe.RecipeState == "0")
                    {
                        string invoiceNo = drugRecipe.InvoiceNO;
                        string recipeNo = drugRecipe.RecipeNO;
                        if (drugStoreManager.UpdateDrugRecipePrintState("1", invoiceNo, recipeNo) == -1)
                        {
                            return -1;
                        }
                    }

                    if (frmLangueChoose.Langue == "2")
                    {
                        //口服药
                        ArrayList alPO = new ArrayList();

                        //外用药
                        ArrayList alUnPO = new ArrayList();
                        foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
                        {
                            //if (applyOut.Usage.Name.ToString() == "口服" || applyOut.Usage.Name.ToString() == "Oral")
                            if (FS.SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(applyOut.Usage.ID))
                            {
                                alPO.Add(applyOut);
                            }
                            else
                            {
                                alUnPO.Add(applyOut);
                            }
                        }
                        if (alPO.Count > 0)
                        {
                            DateTime printTime2 = DateTime.Now;
                            GJLocal.HISFC.Components.OpGuide.DrugStore.BLE.ucReicpeInfoLabelJPoBLE ucReicpeInfoLabelJPo = new GJLocal.HISFC.Components.OpGuide.DrugStore.BLE.ucReicpeInfoLabelJPoBLE();// {7DBB85BF-547C-4230-8598-55A2A4AD83F4}
                            ucReicpeInfoLabelJPo.PrintDrugLabel(alPO, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, "", printTime2);
                        }
                        if (alUnPO.Count > 0)
                        {
                            DateTime printTime3 = DateTime.Now;
                            GJLocal.HISFC.Components.OpGuide.DrugStore.BLE.ucReicpeInfoLabelJOutUseBLE ucReicpeInfoLabelJOutUse = new GJLocal.HISFC.Components.OpGuide.DrugStore.BLE.ucReicpeInfoLabelJOutUseBLE();// {7DBB85BF-547C-4230-8598-55A2A4AD83F4}
                            ucReicpeInfoLabelJOutUse.PrintDrugLabel(alUnPO, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, "", printTime3);
                        }
                    }
                    else
                    {
                        DateTime printTime4 = DateTime.Now;
                        GJLocal.HISFC.Components.OpGuide.DrugStore.BLE.ucReicpeInfoLabelEBLE ucReicpeInfoLabelE = new GJLocal.HISFC.Components.OpGuide.DrugStore.BLE.ucReicpeInfoLabelEBLE();// {7DBB85BF-547C-4230-8598-55A2A4AD83F4}
                        ucReicpeInfoLabelE.PrintDrugLabel(alData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, "", printTime4);
                    }
                }

                #endregion
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
