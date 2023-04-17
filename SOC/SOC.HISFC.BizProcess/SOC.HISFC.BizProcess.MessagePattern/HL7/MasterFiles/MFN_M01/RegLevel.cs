using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01
{
    public class RegLevel
    {
        private NHapi.Model.V24.Message.MFN_M01 ConvertToHL7Object(FS.HISFC.Models.Registration.RegLvlFee regLevel, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType)
        {

            FS.HISFC.BizLogic.Registration.RegLvlFee regLevelFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();

            FS.HISFC.Models.Registration.RegLvlFee regLvlFee = new FS.HISFC.Models.Registration.RegLvlFee();
            regLvlFee = regLevelFeeMgr.Get("1", regLevel.RegLevel.ID);
           
         

            NHapi.Model.V24.Message.MFN_M01 HL7RegLevel = new NHapi.Model.V24.Message.MFN_M01();
            FS.HL7Message.V24.Function.GenerateMSH(HL7RegLevel.MSH);

            //MSH
            HL7RegLevel.MSH.MessageType.MessageType.Value = "MFN";
            HL7RegLevel.MSH.MessageType.TriggerEvent.Value = "M01";

            //MFI
            //主文件标识符
            HL7RegLevel.MFI.MasterFileIdentifier.Identifier.Value = "RegLevel";
            HL7RegLevel.MFI.MasterFileIdentifier.Text.Value = "挂号级别信息";
            HL7RegLevel.MFI.MasterFileIdentifier.NameOfCodingSystem.Value = "HIS";

            //文件层事件代码
            HL7RegLevel.MFI.FileLevelEventCode.Value = "UPD";
            HL7RegLevel.MFI.EnteredDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            HL7RegLevel.MFI.EffectiveDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");

            //应答层代码
            HL7RegLevel.MFI.ResponseLevelCode.Value = "AL";

            //MFE
            NHapi.Model.V24.Group.MFN_M01_MF MF = HL7RegLevel.GetMF(0);

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
            ((NHapi.Base.Model.GenericPrimitive)primaryKeyValue.Data).Value = regLevel.ID;

            NHapi.Model.V24.Datatype.ID primaryKeyValueType = MF.MFE.GetPrimaryKeyValueType(0);
            primaryKeyValueType.Value = "CE";//编码元素

            /*  Z段包含内容
            挂号级别代码	ST	R	3
            挂号级别名称	ST	O	20
            显示顺序	NM	O	4
            是否有效	ST	O	1
            是否专家号	ST	O	1
            是否专科号	ST	O	1
            是否特诊号	ST	O	1
            是否默认项	ST	O	1
            是否急诊	ST	O	1
            操作员	ST	O	6
            操作时间	TS	O	26
            挂号费       NM   O 10
            诊查费       NM   O 10              
             */
            //Z段字段名称
            MF.Zxx.Name = "Z12";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(10, "").Value = regLevel.RegLevel.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(4, "").Value = regLevel.RegLevel.Name;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = regLevel.RegLevel.SortID.ToString();

            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(1, "").Value = regLevel.RegLevel.IsValid ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(1, "").Value = regLevel.RegLevel.IsExpert ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(1, "").Value = regLevel.RegLevel.IsFaculty ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(1, "").Value = regLevel.RegLevel.IsSpecial ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(1, "").Value = regLevel.RegLevel.IsDefault ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(1, "").Value = regLevel.RegLevel.IsEmergency ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = FS.FrameWork.Management.Connection.Operator.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(10, "").Value = regLvlFee.RegFee.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(10, "").Value = regLvlFee.OwnDigFee.ToString();



            return HL7RegLevel;
        }

        public int Send(System.Collections.ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType, ref List<NHapi.Model.V24.Message.MFN_M01> listMFN_M01, ref string errInfo)
        {
            if (alInfo == null)
            {
                errInfo = "包含挂号级别信息的数组为null";
                return -1;
            }
            if (alInfo.Count == 0)
            {
                errInfo = "包含挂号级别信息的数组没有元素";
                return 0;
            }

            foreach (FS.HISFC.Models.Registration.RegLvlFee regLevel in alInfo)
            {
                if (operType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.AddAfterDelete)
                {
                    NHapi.Model.V24.Message.MFN_M01 HL7RegLevel = this.ConvertToHL7Object(regLevel, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete);
                    listMFN_M01.Add(HL7RegLevel);

                    HL7RegLevel = this.ConvertToHL7Object(regLevel, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add);
                    listMFN_M01.Add(HL7RegLevel);
                }
                else
                {
                    NHapi.Model.V24.Message.MFN_M01 HL7RegLevel = this.ConvertToHL7Object(regLevel, operType);
                    listMFN_M01.Add(HL7RegLevel);
                }
            }
            return alInfo.Count;
        }

    }
}

