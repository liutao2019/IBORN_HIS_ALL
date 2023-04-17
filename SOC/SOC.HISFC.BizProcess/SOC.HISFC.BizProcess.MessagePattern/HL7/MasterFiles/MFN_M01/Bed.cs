using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01
{
    public class Bed
    {
        private NHapi.Model.V24.Message.MFN_M01 ConvertToHL7Object(FS.HISFC.Models.Base.Bed bed, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType)
        {
            NHapi.Model.V24.Message.MFN_M01 HL7Bed = new NHapi.Model.V24.Message.MFN_M01();
            FS.HL7Message.V24.Function.GenerateMSH(HL7Bed.MSH);

            //MSH
            HL7Bed.MSH.MessageType.MessageType.Value = "MFN";
            HL7Bed.MSH.MessageType.TriggerEvent.Value = "M01";

            //MFI
            //主文件标识符
            HL7Bed.MFI.MasterFileIdentifier.Identifier.Value = "Bed";
            HL7Bed.MFI.MasterFileIdentifier.Text.Value = "床位信息";
            HL7Bed.MFI.MasterFileIdentifier.NameOfCodingSystem.Value = "HIS";

            //文件层事件代码
            HL7Bed.MFI.FileLevelEventCode.Value = "UPD";
            HL7Bed.MFI.EnteredDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            HL7Bed.MFI.EffectiveDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");

            //应答层代码
            HL7Bed.MFI.ResponseLevelCode.Value = "AL";

            //MFE
            NHapi.Model.V24.Group.MFN_M01_MF MF = HL7Bed.GetMF(0);

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
            ((NHapi.Base.Model.GenericPrimitive)primaryKeyValue.Data).Value = bed.ID;

            NHapi.Model.V24.Datatype.ID primaryKeyValueType = MF.MFE.GetPrimaryKeyValueType(0);
            primaryKeyValueType.Value = "CE";//编码元素

            /*  Z段包含内容
            床位编码	    ST	R	10
            护士站代码	    ST	O	4
            床位等级编码	ST	O	20
            床位编制	    ST	O	1
            床位状态	    ST	O	1
            病室号	        ST	O	10
            医师代码	    ST	O	6
            病床电话	    ST	O	14
            归属	        ST	O	100
            医疗流水号	    ST	O	14
            出院日期(预约)	TS	O	26
            有效性标识	    ST	O	1
            预约标志	    ST	O	1
            顺序号	        NM	O	4
            护理组	        ST	O	20
            操作员	        ST	O	6
            操作日期	    TS	O	26

             */
            //Z段字段名称
            MF.Zxx.Name = "Z07";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(10, "").Value = bed.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(4, "").Value = bed.NurseStation.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = bed.BedGrade.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = bed.BedRankEnumService.ID.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = bed.Status.ID.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(10, "").Value = bed.SickRoom.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = bed.Doctor.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(14, "").Value = bed.Phone;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(100, "").Value = bed.OwnerPc;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(14, "").Value = bed.InpatientNO;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = bed.PrepayOutdate.ToString("yyyyMMddHHmmss");
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = bed.IsValid ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(1, "").Value = bed.IsPrepay ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = bed.SortID.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(20, "").Value = bed.TendGroup;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = FS.FrameWork.Management.Connection.Operator.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");



            return HL7Bed;
        }

        public int Send(System.Collections.ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType, ref List<NHapi.Model.V24.Message.MFN_M01> listMFN_M01, ref string errInfo)
        {
            if (alInfo == null)
            {
                errInfo = "包含床位信息的数组为null";
                return -1;
            }
            if (alInfo.Count == 0)
            {
                errInfo = "包含床位信息的数组没有元素";
                return 0;
            }

            foreach (FS.HISFC.Models.Base.Bed bed in alInfo)
            {
                NHapi.Model.V24.Message.MFN_M01 HL7Bed = this.ConvertToHL7Object(bed, operType);
                listMFN_M01.Add(HL7Bed);
            }

            return listMFN_M01.Count;
        }

    }
}
