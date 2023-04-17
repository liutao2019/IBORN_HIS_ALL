using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01
{
    public class Pact
    {
        private NHapi.Model.V24.Message.MFN_M01 ConvertToHL7Object(FS.HISFC.Models.Base.PactInfo pact, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType)
        {
            NHapi.Model.V24.Message.MFN_M01 HL7Pact = new NHapi.Model.V24.Message.MFN_M01();
            FS.HL7Message.V24.Function.GenerateMSH(HL7Pact.MSH);

            //MSH
            HL7Pact.MSH.MessageType.MessageType.Value = "MFN";
            HL7Pact.MSH.MessageType.TriggerEvent.Value = "M01";

            //MFI
            //主文件标识符
            HL7Pact.MFI.MasterFileIdentifier.Identifier.Value = "Pact";
            HL7Pact.MFI.MasterFileIdentifier.Text.Value = "合同单位信息";
            HL7Pact.MFI.MasterFileIdentifier.NameOfCodingSystem.Value = "HIS";

            //文件层事件代码
            HL7Pact.MFI.FileLevelEventCode.Value = "UPD";
            HL7Pact.MFI.EnteredDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            HL7Pact.MFI.EffectiveDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");

            //应答层代码
            HL7Pact.MFI.ResponseLevelCode.Value = "AL";

            //MFE
            NHapi.Model.V24.Group.MFN_M01_MF MF = HL7Pact.GetMF(0);

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
            ((NHapi.Base.Model.GenericPrimitive)primaryKeyValue.Data).Value = pact.ID;

            NHapi.Model.V24.Datatype.ID primaryKeyValueType = MF.MFE.GetPrimaryKeyValueType(0);
            primaryKeyValueType.Value = "CE";//编码元素

            /*  Z段包含内容
            合同单位编码	ST	R	10
            合同单位名称	ST	O	50
            类别	ST	O	1
            顺序号	NM	O	4
            操作员	ST	O	6
            操作时间	TS	O	26
             */
            //Z段字段名称
            MF.Zxx.Name = "Z08";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(10, "").Value = pact.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(4, "").Value = pact.Name;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = pact.PactSystemType;
    
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = pact.SortID.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = FS.FrameWork.Management.Connection.Operator.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");



            return HL7Pact;
        }

        public int Send(System.Collections.ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType, ref List<NHapi.Model.V24.Message.MFN_M01> listMFN_M01, ref string errInfo)
        {
            if (alInfo == null)
            {
                errInfo = "包含合同单位信息的数组为null";
                return -1;
            }
            if (alInfo.Count == 0)
            {
                errInfo = "包含合同单位信息的数组没有元素";
                return 0;
            }

            foreach (FS.HISFC.Models.Base.PactInfo pact in alInfo)
            {
                NHapi.Model.V24.Message.MFN_M01 HL7Pact = this.ConvertToHL7Object(pact, operType);
                listMFN_M01.Add(HL7Pact);
            }
            return alInfo.Count;
        }

    }
}
