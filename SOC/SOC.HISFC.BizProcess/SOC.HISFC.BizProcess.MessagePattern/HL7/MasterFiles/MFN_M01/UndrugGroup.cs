using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01
{
    public class UndrugGroup
    {
        private NHapi.Model.V24.Message.MFN_M01 ConvertToHL7Object(FS.HISFC.Models.Fee.Item.UndrugComb undrug, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType)
        {
            NHapi.Model.V24.Message.MFN_M01 HL7Undrug = new NHapi.Model.V24.Message.MFN_M01();
            FS.HL7Message.V24.Function.GenerateMSH(HL7Undrug.MSH);

            //MSH
            HL7Undrug.MSH.MessageType.MessageType.Value = "MFN";
            HL7Undrug.MSH.MessageType.TriggerEvent.Value = "M01";

            //MFI
            //主文件标识符
            HL7Undrug.MFI.MasterFileIdentifier.Identifier.Value = "UndrugGroup";
            HL7Undrug.MFI.MasterFileIdentifier.Text.Value = "非药品套餐信息";
            HL7Undrug.MFI.MasterFileIdentifier.NameOfCodingSystem.Value = "HIS";

            //文件层事件代码
            HL7Undrug.MFI.FileLevelEventCode.Value = "UPD";
            HL7Undrug.MFI.EnteredDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            HL7Undrug.MFI.EffectiveDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");

            //应答层代码
            HL7Undrug.MFI.ResponseLevelCode.Value = "AL";

            //MFE
            NHapi.Model.V24.Group.MFN_M01_MF MF = HL7Undrug.GetMF(0);

            //记录层事件代码
            /*  MAD:增加主文件记录
                MDL:从主文件中删除记录
                MUP:更新主文件记录
                MDC:失效：停止使用主文件中的记录，但并不从数据库中删除此记录
                MAC:恢复失效的记录
             */
            MF.MFE.RecordLevelEventCode.Value = undrug.Oper.Memo;

            NHapi.Base.Model.Varies primaryKeyValue = MF.MFE.GetPrimaryKeyValueMFE(0);
            ((NHapi.Base.Model.GenericPrimitive)primaryKeyValue.Data).Value = undrug.Package.ID + undrug.ID;

            NHapi.Model.V24.Datatype.ID primaryKeyValueType = MF.MFE.GetPrimaryKeyValueType(0);
            primaryKeyValueType.Value = "CE";//编码元素

            /*  Z段包含内容
                组套编码	ST	R	12
                组套名称	ST	O	100
             
                非药品编码	ST	R	12
                非药品名称	ST	O	100
             
                输入码	ST	O	14
                有效性	ST	O	1
                数量	NM	O	10
                顺序号	NM	O	4
                操作员	ST	O	6
                操作日期	TS	O	26

             */
            //Z段字段名称
            MF.Zxx.Name = "Z05";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(12, "").Value = undrug.Package.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(100, "").Value = undrug.Package.Name;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(12, "").Value = undrug.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(100, "").Value = undrug.Name;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(14, "").Value = undrug.UserCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = undrug.ValidState;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(10, "").Value = undrug.Qty.ToString();

            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = undrug.SortID.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = FS.FrameWork.Management.Connection.Operator.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(10, "").Value = undrug.SysClass.ID.ToString();

            return HL7Undrug;
        }

        public int Send(System.Collections.ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType, ref List<NHapi.Model.V24.Message.MFN_M01> listMFN_M01, ref string errInfo)
        {
            if (alInfo == null)
            {
                errInfo = "包含非药品复合项目信息的数组为null";
                return -1;
            }
            if (alInfo.Count == 0)
            {
                errInfo = "包含非药品复合项目信息的数组没有元素";
                return 0;
            }
            if (operType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.AddAfterDelete)
            {
                NHapi.Model.V24.Message.MFN_M01 HL7Undrug = null;

                foreach (FS.HISFC.Models.Fee.Item.UndrugComb undrug in alInfo)
                {
                    HL7Undrug = this.ConvertToHL7Object(undrug, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete);
                    listMFN_M01.Add(HL7Undrug);

                    //if (undrug.IsValid)//有效的才发送
                    {
                        HL7Undrug = this.ConvertToHL7Object(undrug, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add);
                        listMFN_M01.Add(HL7Undrug);
                    }
                  
                }
            }
            else
            {
                NHapi.Model.V24.Message.MFN_M01 HL7Undrug = null;
                foreach (FS.HISFC.Models.Fee.Item.UndrugComb undrug in alInfo)
                {
                    //if (undrug.IsValid)
                    {
                        HL7Undrug = this.ConvertToHL7Object(undrug, operType);
                        listMFN_M01.Add(HL7Undrug);
                    }
                }
            }

            return alInfo.Count;
        }

    }
}