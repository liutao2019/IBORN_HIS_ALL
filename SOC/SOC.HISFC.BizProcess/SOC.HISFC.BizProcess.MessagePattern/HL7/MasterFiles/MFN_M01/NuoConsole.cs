using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01
{
    public class NuoConsole
    {
        private NHapi.Model.V24.Message.MFN_M01 ConvertToHL7Object(FS.HISFC.Models.Nurse.Seat console, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType)
        {
            NHapi.Model.V24.Message.MFN_M01 HL7Console = new NHapi.Model.V24.Message.MFN_M01();
            FS.HL7Message.V24.Function.GenerateMSH(HL7Console.MSH);

            //MSH
            HL7Console.MSH.MessageType.MessageType.Value = "MFN";
            HL7Console.MSH.MessageType.TriggerEvent.Value = "M01";

            //MFI
            //主文件标识符
            HL7Console.MFI.MasterFileIdentifier.Identifier.Value = "NuoConsole";
            HL7Console.MFI.MasterFileIdentifier.Text.Value = "分诊诊台信息";
            HL7Console.MFI.MasterFileIdentifier.NameOfCodingSystem.Value = "HIS";

            //文件层事件代码
            HL7Console.MFI.FileLevelEventCode.Value = "UPD";
            HL7Console.MFI.EnteredDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            HL7Console.MFI.EffectiveDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");

            //应答层代码
            HL7Console.MFI.ResponseLevelCode.Value = "AL";

            //MFE
            NHapi.Model.V24.Group.MFN_M01_MF MF = HL7Console.GetMF(0);

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
            ((NHapi.Base.Model.GenericPrimitive)primaryKeyValue.Data).Value = console.ID;

            NHapi.Model.V24.Datatype.ID primaryKeyValueType = MF.MFE.GetPrimaryKeyValueType(0);
            primaryKeyValueType.Value = "CE";//编码元素

            /*  Z段包含内容
            诊台代码	ST	O	6
            诊台名称	ST	O	20
            诊室代码	ST	O	4
            诊室名称	ST	O	20
            有效性	    ST	O	1
            进诊人数	NM	O	
            备注	    ST	O	50
            操作员	    ST	O	6
            操作时间	TS	O	26

             */
            //Z段字段名称
            MF.Zxx.Name = "Z14";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = console.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = console.Name;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(4, "").Value = console.PRoom.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = console.PRoom.Name;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = "1";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(10, "").Value = console.CurrentCount.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(50, "").Value = console.Memo;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = FS.FrameWork.Management.Connection.Operator.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");


            return HL7Console;
        }

        public int Send(System.Collections.ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType, ref List<NHapi.Model.V24.Message.MFN_M01> listMFN_M01, ref string errInfo)
        {
            if (alInfo == null)
            {
                errInfo = "包含诊台信息的数组为null";
                return -1;
            }
            if (alInfo.Count == 0)
            {
                errInfo = "包含诊台信息的数组没有元素";
                return 0;
            }

            foreach (FS.HISFC.Models.Nurse.Seat console in alInfo)
            {
                if (operType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.AddAfterDelete)
                {
                    NHapi.Model.V24.Message.MFN_M01 HL7Console = this.ConvertToHL7Object(console, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete);
                    listMFN_M01.Add(HL7Console);

                    HL7Console = this.ConvertToHL7Object(console, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add);
                    listMFN_M01.Add(HL7Console);
                }
                else
                {
                    NHapi.Model.V24.Message.MFN_M01 HL7Console = this.ConvertToHL7Object(console, operType);
                    listMFN_M01.Add(HL7Console);
                }
            }
            return alInfo.Count;
        }

    }
}



