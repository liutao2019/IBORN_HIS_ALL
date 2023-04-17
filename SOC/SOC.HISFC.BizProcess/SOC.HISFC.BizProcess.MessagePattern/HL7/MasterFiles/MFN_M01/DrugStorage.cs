using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01
{
    public class DrugStorage
    {

        private NHapi.Model.V24.Message.MFN_M01 ConvertToHL7Object(FS.HISFC.Models.Pharmacy.Storage drugStorage, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType)
        {
            NHapi.Model.V24.Message.MFN_M01 HL7Drug = new NHapi.Model.V24.Message.MFN_M01();
            FS.HL7Message.V24.Function.GenerateMSH(HL7Drug.MSH);

            //MSH
            HL7Drug.MSH.MessageType.MessageType.Value = "MFN";
            HL7Drug.MSH.MessageType.TriggerEvent.Value = "M01";

            //MFI
            //主文件标识符
            HL7Drug.MFI.MasterFileIdentifier.Identifier.Value = "Drug";
            HL7Drug.MFI.MasterFileIdentifier.Text.Value = "药品信息";
            HL7Drug.MFI.MasterFileIdentifier.NameOfCodingSystem.Value = "HIS";

            //文件层事件代码
            HL7Drug.MFI.FileLevelEventCode.Value = "UPD";
            HL7Drug.MFI.EnteredDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            HL7Drug.MFI.EffectiveDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");

            //应答层代码
            HL7Drug.MFI.ResponseLevelCode.Value = "AL";

            //MFE
            NHapi.Model.V24.Group.MFN_M01_MF MF = HL7Drug.GetMF(0);

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
            ((NHapi.Base.Model.GenericPrimitive)primaryKeyValue.Data).Value = drugStorage.StockDept.ID+drugStorage.Item.ID;

            NHapi.Model.V24.Datatype.ID primaryKeyValueType = MF.MFE.GetPrimaryKeyValueType(0);
            primaryKeyValueType.Value = "CE";//编码元素

            /*  Z段包含内容
                DRUG_DEPT_CODE	库房编码	ST	R	4
                DRUG_CODE	药品编码	ST	O	12
                LACK_FLAG	门诊缺药标志 0-否，1-是	ST	O	1
                LACK_INPATIENT_FLAG	住院缺药标志 0否 1是	ST	O	1
                OUTPATIENT_USE_FLAG	门诊使用标志 0否 1是	ST	O	1
                INPATIENT_USE_FLAG	住院使用标志 0否 1是	ST	O	1
                RADIX_FLAG	基数药标志 0否 1是	ST	O	1
                VALID_STATE	有效性状态 1 在用 0 停用 2 废弃	ST	O	1

              
             */
            //Z段字段名称
            MF.Zxx.Name = "Z06";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(4, "").Value = drugStorage.StockDept.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(12, "").Value = drugStorage.Item.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = drugStorage.IsLack ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = drugStorage.IsLackForInpatient ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = drugStorage.IsUseForOutpatient ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = drugStorage.IsUseForInpatient ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = drugStorage.IsRadix ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = drugStorage.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid ? "1" : "0";
            return HL7Drug;
        }

        public int Send(System.Collections.ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType, ref List<NHapi.Model.V24.Message.MFN_M01> listMFN_M01, ref string errInfo)
        {
            if (alInfo == null)
            {
                errInfo = "包含药品库存信息的数组为null";
                return -1;
            }
            if (alInfo.Count == 0)
            {
                errInfo = "包含药品库存信息的数组没有元素";
                return 0;
            }

            foreach (FS.HISFC.Models.Pharmacy.Storage drugStorage in alInfo)
            {
                NHapi.Model.V24.Message.MFN_M01 HL7Drug = this.ConvertToHL7Object(drugStorage, operType);
                listMFN_M01.Add(HL7Drug);
            }
            return alInfo.Count;
        }
    }
}
