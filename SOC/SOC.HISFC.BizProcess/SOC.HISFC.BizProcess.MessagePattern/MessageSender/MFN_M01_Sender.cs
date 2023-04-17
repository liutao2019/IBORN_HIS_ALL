using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.SOC.HISFC.BizProcess.MessagePattern.HL7;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageSender
{
    public class MFN_M01_Sender :AbstractSender<ArrayList,NHapi.Model.V24.Message.MFN_M01[],NHapi.Model.V24.Message.ACK,FS.FrameWork.Models.NeuObject>
    {
        private bool CheckConstantIsNeedSend(string constantType)
        {
            if (string.IsNullOrEmpty(constantType))
            {
                return false;
            }
            if (SOC.Public.Char.IsNumber(constantType[0]))
            {
                return false;
            }
            if (!System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\MessagePatternSetting.xml"))
            {
                SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\MessagePatternSetting.xml", "Constant", "USAGE", "False");
            }
            return FS.FrameWork.Function.NConvert.ToBoolean(SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\MessagePatternSetting.xml", "Constant", constantType, "True"));
        }

        protected override int ConvertObjectToSendMessage(ArrayList alInfo, ref NHapi.Model.V24.Message.MFN_M01[] e, params object[] appendParams)
        {

            List<NHapi.Model.V24.Message.MFN_M01> listMFN_M01 = new List<NHapi.Model.V24.Message.MFN_M01>();
            FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType = (FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType)appendParams[0];
            FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType infoType = (FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType)appendParams[1];
            int i = -1;

            if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Department)
            {
                HL7.MasterFiles.MFN_M01.Department deptMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.Department();
                i= deptMgr.Send(alInfo, operType,ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Employee)
            {
                HL7.MasterFiles.MFN_M01.Employee employeeMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.Employee();
                i = employeeMgr.Send(alInfo, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DeptStat)
            {
                HL7.MasterFiles.MFN_M01.DeptStat deptStatMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.DeptStat();
                i = deptStatMgr.Send(alInfo, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Undrug)
            {
                HL7.MasterFiles.MFN_M01.Undrug undrugMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.Undrug();
                i = undrugMgr.Send(alInfo, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.UndrugGroup)
            {
                HL7.MasterFiles.MFN_M01.UndrugGroup undrugGroupMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.UndrugGroup();
                i = undrugGroupMgr.Send(alInfo, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Bed)
            {
                HL7.MasterFiles.MFN_M01.Bed bedMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.Bed();
                i = bedMgr.Send(alInfo, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Pact)
            {
                HL7.MasterFiles.MFN_M01.Pact pactMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.Pact();
                i = pactMgr.Send(alInfo, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Schema)
            {
                HL7.MasterFiles.MFN_M01.Schema schemaMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.Schema();
                i = schemaMgr.Send(alInfo, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.RegLevel)
            {
                HL7.MasterFiles.MFN_M01.RegLevel regLevelMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.RegLevel();
                i = regLevelMgr.Send(alInfo, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.NuoRoom)
            {
                HL7.MasterFiles.MFN_M01.NuoRoom nuoRoomMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.NuoRoom();
                i = nuoRoomMgr.Send(alInfo, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.NuoConsole)
            {
                HL7.MasterFiles.MFN_M01.NuoConsole nuoConsoleMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.NuoConsole();
                i = nuoConsoleMgr.Send(alInfo, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Drug)
            {
                HL7.MasterFiles.MFN_M01.Drug drugMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.Drug();
                i = drugMgr.Send(alInfo, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.ICD10)
            {
                HL7.MasterFiles.MFN_M01.ICD10 drugMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.ICD10();
                i = drugMgr.Send(alInfo, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DrugStorage)
            {
                HL7.MasterFiles.MFN_M01.DrugStorage drugMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.DrugStorage();
                i = drugMgr.Send(alInfo, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DrugCompare)
            {
                HL7.MasterFiles.MFN_M01.DrugCompare drugcompareMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.DrugCompare();
                i = drugcompareMgr.Send(alInfo, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.SICompare)
            {
                HL7.MasterFiles.MFN_M01.SICompare sicompareMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.SICompare();
                i = sicompareMgr.Send(alInfo, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Constant)
            {
                if (alInfo == null)
                {
                    errInfo = "包含常数信息的数组为null";
                    return -1;
                }
                if (alInfo.Count == 0)
                {
                    errInfo = "包含常数信息的数组没有元素";
                    return 0;
                }
                FS.HISFC.Models.Base.Const constant = alInfo[0] as FS.HISFC.Models.Base.Const;

                if (!this.CheckConstantIsNeedSend(constant.OperEnvironment.Memo))
                {
                    return 0;
                }
                HL7.MasterFiles.MFN_M01.Constant constantMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.Constant();
                i = constantMgr.Send(alInfo, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DrugCompany)
            {
                System.Collections.ArrayList alConstant = new System.Collections.ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.Company company in alInfo)
                {
                    FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                    con.ID = company.ID;
                    con.Name = company.Name;
                    con.SortID = 0;
                    con.OperEnvironment = company.Oper;
                    con.WBCode = company.WBCode;
                    con.SpellCode = company.SpellCode;
                    con.UserCode = company.UserCode;
                    if (company.Type == "0")
                    {
                        con.OperEnvironment.Memo = "PhaCompany";
                    }
                    else if (company.Type == "1")
                    {
                        con.OperEnvironment.Memo = "PhaProduct";
                    }
                    else
                    {
                        continue;
                    }
                    alConstant.Add(con);
                }

                HL7.MasterFiles.MFN_M01.Constant constantMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.Constant();
                i = constantMgr.Send(alConstant, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DrugFunction)
            {
                System.Collections.ArrayList alConstant = new System.Collections.ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.PhaFunction phyFunction in alInfo)
                {
                    FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                    con.ID = phyFunction.ID;
                    con.Name = phyFunction.Name;
                    con.SortID = 0;
                    con.OperEnvironment = phyFunction.Oper;
                    con.WBCode = phyFunction.WBCode;
                    con.SpellCode = phyFunction.SpellCode;
                    con.UserCode = phyFunction.UserCode;
                    con.OperEnvironment.Memo = "PhaFunction";
                    alConstant.Add(con);
                }

                HL7.MasterFiles.MFN_M01.Constant constantMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.Constant();
                i = constantMgr.Send(alConstant, operType, ref listMFN_M01, ref errInfo);
            }
            else if (infoType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DrugBillClass)
            {
                System.Collections.ArrayList alConstant = new System.Collections.ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass in alInfo)
                {
                    FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                    con.ID = drugBillClass.ID;
                    con.Name = drugBillClass.Name;
                    con.SortID = 0;
                    con.OperEnvironment = drugBillClass.Oper;
                    con.OperEnvironment.Memo = "BillClass";
                    alConstant.Add(con);
                }

                HL7.MasterFiles.MFN_M01.Constant constantMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.Constant();
                i = constantMgr.Send(alConstant, operType, ref listMFN_M01, ref errInfo);
            }

            e = listMFN_M01.ToArray();
            return 1;
        }
    }
}