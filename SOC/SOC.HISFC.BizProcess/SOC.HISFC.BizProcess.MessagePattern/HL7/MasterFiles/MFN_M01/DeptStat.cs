using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01
{
    public class DeptStat
    {
        private NHapi.Model.V24.Message.MFN_M01 ConvertToHL7Object(FS.HISFC.Models.Base.DepartmentStat dept, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType)
        {
            NHapi.Model.V24.Message.MFN_M01 HL7Dept = new NHapi.Model.V24.Message.MFN_M01();
            FS.HL7Message.V24.Function.GenerateMSH(HL7Dept.MSH);

            //MSH
            HL7Dept.MSH.MessageType.MessageType.Value = "MFN";
            HL7Dept.MSH.MessageType.TriggerEvent.Value = "M01";

            //MFI
            //主文件标识符
            HL7Dept.MFI.MasterFileIdentifier.Identifier.Value = "DeptStat";
            HL7Dept.MFI.MasterFileIdentifier.Text.Value = "科室病区关系";
            HL7Dept.MFI.MasterFileIdentifier.NameOfCodingSystem.Value = "HIS";

            //文件层事件代码
            HL7Dept.MFI.FileLevelEventCode.Value = "UPD";
            HL7Dept.MFI.EnteredDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            HL7Dept.MFI.EffectiveDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");

            //应答层代码
            HL7Dept.MFI.ResponseLevelCode.Value = "AL";

            //MFE
            NHapi.Model.V24.Group.MFN_M01_MF MF = HL7Dept.GetMF(0);

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
            ((NHapi.Base.Model.GenericPrimitive)primaryKeyValue.Data).Value = dept.ID;

            NHapi.Model.V24.Datatype.ID primaryKeyValueType = MF.MFE.GetPrimaryKeyValueType(0);
            primaryKeyValueType.Value = "CE";//编码元素

            /*  Z段包含内容
             
            主键列	                    ST	R	12
            父级编码（或父级分类编码）	ST	O	4
            父级名称（或父级分类名称）	ST	O	16
            科室编码	                ST	O	4
            有效性标志	                ST	O	1
            操作员	                    ST	O	6
            操作时间	                TS	O	26

             */
            //Z段字段名称
            MF.Zxx.Name = "Z03";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(12, "").Value = dept.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(4, "").Value = dept.PardepCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = dept.PardepName;
         
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = dept.DeptCode.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = (dept.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid) ? "1" : "0"; //add nieaj 2012-05-18

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = FS.FrameWork.Management.Connection.Operator.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");



            return HL7Dept;
        }

        public int Send(System.Collections.ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType, ref List<NHapi.Model.V24.Message.MFN_M01> listMFN_M01, ref string errInfo)
        {
            if (alInfo == null)
            {
                errInfo = "包含科室结构信息的数组为null";
                return -1;
            }
            if (alInfo.Count == 0)
            {
                errInfo = "包含科室结构信息的数组没有元素";
                return 0;
            }

            foreach (FS.HISFC.Models.Base.DepartmentStat dept in alInfo)
            {
                //暂时只传送科室病区关系
                if (dept.StatCode != "01")
                {
                    continue;
                }
                if (dept.PardepCode == "AAAA")
                {
                    continue;
                }
                NHapi.Model.V24.Message.MFN_M01 HL7Dept = this.ConvertToHL7Object(dept, operType);
                listMFN_M01.Add(HL7Dept);
            }
            return alInfo.Count;
        }

    }
}
