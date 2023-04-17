using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01
{
    public class NuoRoom
    {
        private NHapi.Model.V24.Message.MFN_M01 ConvertToHL7Object(FS.HISFC.Models.Nurse.Room room, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType)
        {
            NHapi.Model.V24.Message.MFN_M01 HL7Room = new NHapi.Model.V24.Message.MFN_M01();
            FS.HL7Message.V24.Function.GenerateMSH(HL7Room.MSH);

            //MSH
            HL7Room.MSH.MessageType.MessageType.Value = "MFN";
            HL7Room.MSH.MessageType.TriggerEvent.Value = "M01";

            //MFI
            //主文件标识符
            HL7Room.MFI.MasterFileIdentifier.Identifier.Value = "NuoRoom";
            HL7Room.MFI.MasterFileIdentifier.Text.Value = "分诊诊室信息";
            HL7Room.MFI.MasterFileIdentifier.NameOfCodingSystem.Value = "HIS";

            //文件层事件代码
            HL7Room.MFI.FileLevelEventCode.Value = "UPD";
            HL7Room.MFI.EnteredDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            HL7Room.MFI.EffectiveDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");

            //应答层代码
            HL7Room.MFI.ResponseLevelCode.Value = "AL";

            //MFE
            NHapi.Model.V24.Group.MFN_M01_MF MF = HL7Room.GetMF(0);

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
            ((NHapi.Base.Model.GenericPrimitive)primaryKeyValue.Data).Value = room.ID;

            NHapi.Model.V24.Datatype.ID primaryKeyValueType = MF.MFE.GetPrimaryKeyValueType(0);
            primaryKeyValueType.Value = "CE";//编码元素

            /*  Z段包含内容
            分诊科室	ST	O	4
            诊室代码	ST	O	6
            诊室名称	ST	O	20
            有效性	    ST	O	1
            显示顺序	NM	O	4
            操作员	    ST	O	6
            操作时间	TS	O	26


             */
            //Z段字段名称
            MF.Zxx.Name = "Z13";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(4, "").Value = room.Dept.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = room.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = room.Name;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = room.IsValid;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = room.Sort.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = FS.FrameWork.Management.Connection.Operator.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");


            return HL7Room;
        }

        public int Send(System.Collections.ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType, ref List<NHapi.Model.V24.Message.MFN_M01> listMFN_M01, ref string errInfo)
        {
            if (alInfo == null)
            {
                errInfo = "包含诊室信息的数组为null";
                return -1;
            }
            if (alInfo.Count == 0)
            {
                errInfo = "包含诊室信息的数组没有元素";
                return 0;
            }

            foreach (FS.HISFC.Models.Nurse.Room room in alInfo)
            {
                if (operType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.AddAfterDelete)
                {
                    NHapi.Model.V24.Message.MFN_M01 HL7Room = this.ConvertToHL7Object(room, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete);
                    listMFN_M01.Add(HL7Room);

                    HL7Room = this.ConvertToHL7Object(room, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add);
                    listMFN_M01.Add(HL7Room);
                }
                else
                {
                    NHapi.Model.V24.Message.MFN_M01 HL7Room = this.ConvertToHL7Object(room, operType);
                    listMFN_M01.Add(HL7Room);
                }
            }
            return alInfo.Count;
        }

    }
}


