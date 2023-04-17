using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.DrugStore.GuangZhou.ZDLY.Outpatient
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
        FS.SOC.Local.DrugStore.GuangZhou.ZDLY.Outpatient.IRON.IronBizlogic ironMgr = new FS.SOC.Local.DrugStore.GuangZhou.ZDLY.Outpatient.IRON.IronBizlogic();

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
        public string GetDiagnose(string clinicNO)
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

            if (drugTerminal == null)
            {
                return -1;
            }

            if (drugRecipe == null)
            {
                return -1;
            }

            if (drugTerminal.TerimalPrintType == FS.HISFC.Models.Pharmacy.EnumClinicPrintType.扩展)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                int param = ironMgr.InsertCompress(drugRecipe, drugTerminal);

                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.ironMgr.WriteErr();
                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
            }

            return 1;
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

            ArrayList alValid = this.GetValid(alData);

            ArrayList alInValid = this.GetInValid(alData);

            if(alValid == null || alValid.Count == 0)
            {
                return -1;
            }

            if (drugTerminal.TerimalPrintType == FS.HISFC.Models.Pharmacy.EnumClinicPrintType.标签)
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
                        alPCC.Add(applyOut);
                    }
                    else
                    {
                        alPZ.Add(applyOut);
                    }
                }

                if (alPCC.Count > 0)
                {
                    Outpatient.ucHerbalDrugList ucHerbalDrugList = new ucHerbalDrugList();
                    ucHerbalDrugList.Print(alPCC, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName, printTime);
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
                    if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSysClass(applyOut.Item.ID) == "PCC")
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
                    Outpatient.ucHerbalDrugList ucHerbalDrugList = new ucHerbalDrugList();
                    ucHerbalDrugList.Print(alPCC, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName, printTime);
                }

                if (alPZ.Count > 0)
                {
                    this.myPrintDrugLabel(alValid, drugRecipe, type, drugTerminal);
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
                        alPCC.Add(applyOut);
                    }
                    else
                    {
                        alPZ.Add(applyOut);
                    }
                }

                if (alPCC.Count > 0)
                {
                    Outpatient.ucHerbalDrugList ucHerbalDrugList = new ucHerbalDrugList();
                    ucHerbalDrugList.Print(alPCC, this.GetDiagnose(drugRecipe.ClinicNO), drugRecipe, drugTerminal, HospitalName, printTime);
                }

                if (alPZ.Count > 0)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    if (drugRecipe.RecipeState == "0")
                    {
                        foreach (FS.HISFC.Models.Pharmacy.ApplyOut ironApply in alPZ)
                        {
                            IRON.Outmedtablehis outmedtablehis = new FS.SOC.Local.DrugStore.GuangZhou.ZDLY.Outpatient.IRON.Outmedtablehis();
                            outmedtablehis = this.GetOutmedtablehis(ironApply, drugRecipe, drugTerminal);
                            if (outmedtablehis == null)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                break;
                            }
                          
                            int param = ironMgr.Insert(outmedtablehis);
                            if (param == -1)
                            {
                                continue;
                            }
                        }                  
                    }
                    else
                    {
                        this.myPrintDrugLabel(alPZ, drugRecipe, type, drugTerminal);
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
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
            return 1;
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

        /// <summary>
        /// 标签打印方法
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugRecipe"></param>
        /// <param name="type"></param>
        /// <param name="drugTerminal"></param>
        /// <returns></returns>
        private int myPrintDrugLabel(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string type, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            DateTime printTime = DateTime.Now;

            Outpatient.ucReicpeInfoLabel ucReicpeInfoLabel = new Outpatient.ucReicpeInfoLabel();
            ucReicpeInfoLabel.PrintDrugLabel(alData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, this.GetDiagnose(drugRecipe.ClinicNO), printTime);

            Outpatient.ucDrugLabel ucDrugLabel = new Outpatient.ucDrugLabel();
            ucDrugLabel.PrintDrugLabel(alData, drugRecipe, drugTerminal, this.IsLabelPrintRegularName, HospitalName, IsPrintMemo, QtyShowType, printTime, "");

            return 1;
        }

        /// <summary>
        /// 根据出库实体，调剂表，终端实体转换为门诊发药机实体
        /// </summary>
        /// <param name="applyOut"></param>
        /// <param name="drugRecipe"></param>
        /// <returns></returns>
        private IRON.Outmedtablehis GetOutmedtablehis(FS.HISFC.Models.Pharmacy.ApplyOut applyOut, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe,FS.HISFC.Models.Pharmacy.DrugTerminal terminal)
        { 
            IRON.Outmedtablehis outmedtablehis = new FS.SOC.Local.DrugStore.GuangZhou.ZDLY.Outpatient.IRON.Outmedtablehis();
            outmedtablehis.PrescriptionNO = drugRecipe.RecipeNO;//处方号
            outmedtablehis.MedID = applyOut.SequenceNO;//方内流水号
            outmedtablehis.MedOnlyCode = applyOut.Item.ID;//药品代码
            outmedtablehis.MedAMT = FS.FrameWork.Function.NConvert.ToInt32(applyOut.Operation.ApplyQty);//申请量
            outmedtablehis.MedName = applyOut.Item.Name;//药品名称
            outmedtablehis.MedUnit = applyOut.Item.Specs.ToString();//规格
            outmedtablehis.MedPack = applyOut.Item.MinUnit;//包装单位
            outmedtablehis.MedConvercof = FS.FrameWork.Function.NConvert.ToInt32(applyOut.Item.PackQty);//默认都拆零
            outmedtablehis.MedFactory = applyOut.Item.Product.Producer.ID;//生产厂家
            outmedtablehis.MedOutTime = drugRecipe.FeeOper.OperTime;//收费时间
            outmedtablehis.WindowNO = FS.FrameWork.Function.NConvert.ToInt32(terminal.SendWindow.ID);//发药窗编码
            outmedtablehis.PatientName = drugRecipe.PatientName.ToString();//病人姓名
            outmedtablehis.PatientSex = drugRecipe.Sex.Name.ToString();//性别
            DateTime nowTime = this.diagnoseMgr.GetDateTimeFromSysDateTime();
            outmedtablehis.SendFlag = '0';//发送标记，默认是0
            if (drugRecipe.Age.ToString("yyyy-MM-dd") == "0001-01-01")
            {
                outmedtablehis.Dateofbirth = nowTime;
            }
            else
            {
                outmedtablehis.Dateofbirth = drugRecipe.Age;//生日
            }
            outmedtablehis.WardNO = drugRecipe.DoctDept.ID.ToString();//科室编码
            outmedtablehis.MedUsage = Common.Function.GetFrequenceName(applyOut.Frequency);//频次名称
            outmedtablehis.Diagnosis = this.GetDiagnose(drugRecipe.ClinicNO);//诊断
            outmedtablehis.MedPerday = "每次" + Common.Function.GetOnceDose(applyOut);//用量
            outmedtablehis.MedPerdos = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut.Usage.ID);//用法名称
            outmedtablehis.DoctorName = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);//医生姓名
            outmedtablehis.WardName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);//科室名称
            outmedtablehis.PatientID = drugRecipe.CardNO;//病人卡号
            outmedtablehis.FPNO1 = drugRecipe.InvoiceNO;//发票号
            outmedtablehis.MedUnitprice = Math.Round(applyOut.Item.PriceCollection.RetailPrice * applyOut.Operation.ApplyQty / applyOut.Item.PackQty,2);//药品金额
            outmedtablehis.Remark = GetRemark(applyOut);//备注           
            return outmedtablehis;
        }

        private string GetRemark(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            string remark = "";
            if (applyOut == null)
            {
                return remark;
            }

            FS.HISFC.Models.Order.OutPatient.Order outPatientOrder = SOC.Local.DrugStore.GuangZhou.Common.Function.GetOrder(applyOut.OrderNO);

            if (outPatientOrder == null)
            {
                return remark;
            }
            else
            { 
                FS.HISFC.Models.Pharmacy.Item item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
                if(item == null)
                {
                    remark = outPatientOrder.Memo;
                }
                else
                {
                    if (string.IsNullOrEmpty(outPatientOrder.Memo))
                    {
                        remark = item.Product.Caution +
                        " 储藏条件:" + item.Product.StoreCondition;
                    }
                    else
                    {
                        remark = outPatientOrder.Memo + "、" + item.Product.Caution +
                            "储藏条件:" + item.Product.StoreCondition;
                    }
                }
                if (remark.Length > 50)
                {
                    return remark.Substring(0, 50);
                }
                else
                {
                    return remark;
                }
            }       
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

        private ArrayList GetInValid(ArrayList alData)
        {
            if (alData == null || alData.Count == 0)
            {
                return new ArrayList();
            }
            ArrayList alInValid = new ArrayList();

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                {
                    alInValid.Add(applyOut);
                }
            }
            return alInValid;
        }

        #region IOutpatientDrug 成员


        public int PrintDrugBag(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            return 0;
        }

        #endregion
    }
}
