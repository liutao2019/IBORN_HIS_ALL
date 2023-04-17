using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01
{
    /// <summary>
    /// ICD10 操作
    /// </summary>
   public  class ICD10
    {
        private NHapi.Model.V24.Message.MFN_M01 ConvertToHL7Object(FS.HISFC.Models.HealthRecord.ICD icd10, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType)
        {
            NHapi.Model.V24.Message.MFN_M01 HL7Constant = new NHapi.Model.V24.Message.MFN_M01();
            FS.HL7Message.V24.Function.GenerateMSH(HL7Constant.MSH);

            //MSH
            HL7Constant.MSH.MessageType.MessageType.Value = "MFN";
            HL7Constant.MSH.MessageType.TriggerEvent.Value = "M01";

            //MFI
            //主文件标识符
            HL7Constant.MFI.MasterFileIdentifier.Identifier.Value = "ICD10";
            HL7Constant.MFI.MasterFileIdentifier.Text.Value = "ICD10信息";
            HL7Constant.MFI.MasterFileIdentifier.NameOfCodingSystem.Value = "HIS";

            //文件层事件代码
            HL7Constant.MFI.FileLevelEventCode.Value = "UPD";
            HL7Constant.MFI.EnteredDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            HL7Constant.MFI.EffectiveDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");

            //应答层代码
            HL7Constant.MFI.ResponseLevelCode.Value = "AL";

            //MFE
            NHapi.Model.V24.Group.MFN_M01_MF MF = HL7Constant.GetMF(0);

            //记录层事件代码
            /*  MAD:增加主文件记录
                MDL:从主文件中删除记录
                MUP:更新主文件记录
                MDC:失效：停止使用主文件中的记录，但并不从数据库中删除此记录
                MAC:恢复失效的记录
             */
            if (operType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add)
            {
                MF.MFE.RecordLevelEventCode.Value = "MAD";
            }
            else if (operType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete)
            {
                MF.MFE.RecordLevelEventCode.Value = "MDL";
            }
            else if (operType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify)
            {
                MF.MFE.RecordLevelEventCode.Value = "MUP";
            }

            NHapi.Base.Model.Varies primaryKeyValue = MF.MFE.GetPrimaryKeyValueMFE(0);
            ((NHapi.Base.Model.GenericPrimitive)primaryKeyValue.Data).Value = icd10.ID;

            NHapi.Model.V24.Datatype.ID primaryKeyValueType = MF.MFE.GetPrimaryKeyValueType(0);
            primaryKeyValueType.Value = "CE";//编码元素

            /*  Z段包含内容
           1	SEQUENCE_NO	主键	ST	O	10
            2	ICD_CODE	icd10主诊断码	ST	O	50
            3	ICD_NAME	中文疾病名称	ST	O	150
            4	DISEASE30_FLAG	30种疾病标志 	ST	O	1
            5	INFECT_FLAG	传染病标志	ST	O	1
            6	CANCER_FLAG	肿瘤标志	ST	O	1
            7	VALID_STATE	有效性标志	ST	O	1
            8	SORT_ID	序号	ST	O	100
            9	OPER_CODE	操作员	ST	O	6
            10	OPER_DATE	操作时间	TS	O	26

             */
            //Z段字段名称
            MF.Zxx.Name = "Z10";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(10, "").Value = icd10.SeqNo;//1
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(50, "").Value = icd10.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(150, "").Value = icd10.Name;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(1, "").Value = icd10.Is30Illness;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = icd10.IsInfection;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = icd10.IsTumour;//6
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = icd10.IsValid?"1":"0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(100, "").Value = "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = icd10.OperInfo.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = icd10.OperInfo.OperTime.ToString("yyyyMMddHHmmss");
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = icd10.SpellCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = icd10.WBCode;

            return HL7Constant;
        }

        public int Send(System.Collections.ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType, ref List<NHapi.Model.V24.Message.MFN_M01> listMFN_M01, ref string errInfo)
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

            foreach (FS.HISFC.Models.HealthRecord.ICD icd in alInfo)
            {
                if (operType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.AddAfterDelete)
                {
                    NHapi.Model.V24.Message.MFN_M01 HL7Constant = this.ConvertToHL7Object(icd, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete);
                    listMFN_M01.Add(HL7Constant);

                    HL7Constant = this.ConvertToHL7Object(icd, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add);
                    listMFN_M01.Add(HL7Constant);
                }
                else
                {
                    NHapi.Model.V24.Message.MFN_M01 HL7Constant = this.ConvertToHL7Object(icd, operType);
                    listMFN_M01.Add(HL7Constant);
                }
            }
            return alInfo.Count;
        }
    }
}
