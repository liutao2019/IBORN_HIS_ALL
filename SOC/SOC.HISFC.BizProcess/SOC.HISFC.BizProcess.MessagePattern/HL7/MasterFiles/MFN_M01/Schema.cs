using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01
{
    public class Schema
    {
        private NHapi.Model.V24.Message.MFN_M01 ConvertToHL7Object(FS.HISFC.Models.Registration.Schema schema, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType)
        {
            NHapi.Model.V24.Message.MFN_M01 HL7Schema = new NHapi.Model.V24.Message.MFN_M01();
            FS.HL7Message.V24.Function.GenerateMSH(HL7Schema.MSH);

            //MSH
            HL7Schema.MSH.MessageType.MessageType.Value = "MFN";
            HL7Schema.MSH.MessageType.TriggerEvent.Value = "M01";

            //MFI
            //主文件标识符
            HL7Schema.MFI.MasterFileIdentifier.Identifier.Value = "OPRSCHEMA";
            HL7Schema.MFI.MasterFileIdentifier.Text.Value = "排班信息";
            HL7Schema.MFI.MasterFileIdentifier.NameOfCodingSystem.Value = "HIS";

            //文件层事件代码
            HL7Schema.MFI.FileLevelEventCode.Value = "UPD";
            HL7Schema.MFI.EnteredDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            HL7Schema.MFI.EffectiveDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");

            //应答层代码
            HL7Schema.MFI.ResponseLevelCode.Value = "AL";

            //MFE
            NHapi.Model.V24.Group.MFN_M01_MF MF = HL7Schema.GetMF(0);

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
            ((NHapi.Base.Model.GenericPrimitive)primaryKeyValue.Data).Value = schema.Templet.ID;

            NHapi.Model.V24.Datatype.ID primaryKeyValueType = MF.MFE.GetPrimaryKeyValueType(0);
            primaryKeyValueType.Value = "CE";//编码元素

            /*  Z段包含内容
            序号	                            ST	R	12
            排班类型	                        ST	O	1
            看诊日期	                        TS	O	26
            星期	                            ST	O	1
            开始时间	                        TS	O	26
            结束时间	                        TS	O	26
            科室代号	                        ST	O	4
            科室名称	                        ST	O	50
            医师代号,当为科室排班时,值为None	ST	O	6
            医生姓名	                        ST	O	16
            医生类型	                        ST	O	2
            来人挂号限额	                    NM	O	10
            挂号已挂	                        NM	O	10
            来电挂号限额	                    NM	O	10
            来电已挂	                        NM	O	10
            来电已预约	                        NM	O	10
            特诊挂号限额	                    NM	O	10
            特诊已挂	                        NM	O	10
            1正常/0停诊	                        ST	O	1
            1加号/0否	                        ST	O	1
            停诊原因	                        ST	O	3
            停诊原因名称	                    ST	O	16
            停止人	                            ST	O	6
            停止时间	                        TS	O	26
            顺序号	                            NM	O	4
            挂号级别代码	                    ST	O	3
            挂号级别名称	                    ST	O	20
            备注	                            ST	O	50
            操作员	                            ST	O	6
            最近改动日期	                    TS	O	26
            午别                                ST  0   2

             */
            //Z段字段名称
            MF.Zxx.Name = "Z11";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(12, "").Value = schema.Templet.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = ((int)schema.Templet.EnumSchemaType).ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = schema.SeeDate.ToString("yyyyMMddHHmmss");

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = ((int)schema.Templet.Week).ToString();

            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = schema.Templet.Begin.ToString("yyyyMMddHHmmss");
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = schema.Templet.End.ToString("yyyyMMddHHmmss");

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(4, "").Value = schema.Templet.Dept.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(50, "").Value = schema.Templet.Dept.Name;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = schema.Templet.Doct.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = schema.Templet.Doct.Name;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(2, "").Value = schema.Templet.DoctType.ID.ToString();

            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(10, "").Value = schema.Templet.RegQuota.ToString("F0");
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(10, "").Value = schema.RegedQTY.ToString("F0");

            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(10, "").Value = schema.Templet.TelQuota.ToString("F0");
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(10, "").Value = schema.TeledQTY.ToString("F0");
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(10, "").Value = schema.TelingQTY.ToString("F0");

            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(10, "").Value = schema.Templet.SpeQuota.ToString("F0");
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(10, "").Value = schema.SpedQTY.ToString("F0");

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = schema.Templet.IsValid ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = schema.Templet.IsAppend ? "1" : "0";

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(3, "").Value = schema.Templet.StopReason.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = schema.Templet.StopReason.Name;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = schema.Templet.Stop.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = schema.Templet.Stop.OperTime.ToString("yyyyMMddHHmmss");

            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = schema.SeeNO.ToString();

            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(3, "").Value = schema.Templet.RegLevel.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(20, "").Value = schema.Templet.RegLevel.Name;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(50, "").Value = schema.Templet.Memo;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = FS.FrameWork.Management.Connection.Operator.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(2, "").Value = schema.Templet.Noon.ID;


            return HL7Schema;
        }

        public int Send(System.Collections.ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType, ref List<NHapi.Model.V24.Message.MFN_M01> listMFN_M01, ref string errInfo)
        {
            if (alInfo == null)
            {
                errInfo = "包含排班信息的数组为null";
                return -1;
            }
            if (alInfo.Count == 0)
            {
                errInfo = "包含排班信息的数组没有元素";
                return 0;
            }

            foreach (FS.HISFC.Models.Registration.Schema schema in alInfo)
            {
                NHapi.Model.V24.Message.MFN_M01 HL7Schema = this.ConvertToHL7Object(schema, operType);
                listMFN_M01.Add(HL7Schema);
            }
            return alInfo.Count;
        }

        public int Receive(NHapi.Model.V24.Message.MFN_M01 MFN, ref string errInfo)
        {
            FS.HISFC.BizLogic.Registration.Schema schMgr = new FS.HISFC.BizLogic.Registration.Schema();
            FS.HISFC.Models.Registration.Schema Schema = new FS.HISFC.Models.Registration.Schema();
            NHapi.Model.V24.Segment.Z11 ZA4 = MFN.GetMF(0).GetStructure<NHapi.Model.V24.Segment.Z11>();
            Schema.Templet.ID = ZA4.Get<NHapi.Model.V24.Datatype.ST>(1).Value;
            if (ZA4.Get<NHapi.Model.V24.Datatype.ST>(2).Value == "1")
                Schema.Templet.EnumSchemaType = FS.HISFC.Models.Base.EnumSchemaType.Doct;
            else
            {
                Schema.Templet.EnumSchemaType = FS.HISFC.Models.Base.EnumSchemaType.Dept;
            }
            Schema.SeeDate = DateTime.ParseExact(ZA4.Get<NHapi.Model.V24.Datatype.TS>(3).TimeOfAnEvent.Value, "yyyyMMdd", null);
            Schema.Templet.Week = DateTime.ParseExact(ZA4.Get<NHapi.Model.V24.Datatype.TS>(3).TimeOfAnEvent.Value, "yyyyMMdd",null).DayOfWeek;
            Schema.Templet.Begin = DateTime.ParseExact(ZA4.Get<NHapi.Model.V24.Datatype.TS>(5).TimeOfAnEvent.Value, "yyyyMMddHHmm",null);
            Schema.Templet.End = DateTime.ParseExact(ZA4.Get<NHapi.Model.V24.Datatype.TS>(6).TimeOfAnEvent.Value, "yyyyMMddHHmm", null);
            Schema.Templet.Dept.ID = ZA4.Get<NHapi.Model.V24.Datatype.ST>(7).Value;
            Schema.Templet.Dept.Name = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetDepartmentName(ZA4.Get<NHapi.Model.V24.Datatype.ST>(7).Value);
            if (Schema.Templet.EnumSchemaType == FS.HISFC.Models.Base.EnumSchemaType.Dept)
            {
                Schema.Templet.Doct.ID = "None";
            }
            else
            {
                Schema.Templet.Doct.ID = ZA4.Get<NHapi.Model.V24.Datatype.ST>(9).Value;
            }
            Schema.Templet.Doct.Name = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetEmployeeName(ZA4.Get<NHapi.Model.V24.Datatype.ST>(9).Value);
            Schema.Templet.DoctType.ID = ZA4.Get<NHapi.Model.V24.Datatype.ST>(11).Value;
            Schema.Templet.RegQuota = FS.FrameWork.Function.NConvert.ToDecimal(ZA4.Get<NHapi.Model.V24.Datatype.NM>(12).Value);
            Schema.RegedQTY = FS.FrameWork.Function.NConvert.ToDecimal(ZA4.Get<NHapi.Model.V24.Datatype.NM>(13).Value);
            Schema.Templet.TelQuota = FS.FrameWork.Function.NConvert.ToDecimal(ZA4.Get<NHapi.Model.V24.Datatype.NM>(14).Value);
            Schema.TeledQTY = FS.FrameWork.Function.NConvert.ToDecimal(ZA4.Get<NHapi.Model.V24.Datatype.NM>(15).Value);
            Schema.TelingQTY = FS.FrameWork.Function.NConvert.ToDecimal(ZA4.Get<NHapi.Model.V24.Datatype.NM>(16).Value);
            Schema.Templet.SpeQuota = FS.FrameWork.Function.NConvert.ToDecimal(ZA4.Get<NHapi.Model.V24.Datatype.NM>(17).Value);
            Schema.SpedQTY = FS.FrameWork.Function.NConvert.ToDecimal(ZA4.Get<NHapi.Model.V24.Datatype.NM>(18).Value);
            Schema.Templet.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(ZA4.Get<NHapi.Model.V24.Datatype.ST>(19).Value);
            Schema.Templet.IsAppend = FS.FrameWork.Function.NConvert.ToBoolean(ZA4.Get<NHapi.Model.V24.Datatype.ST>(20).Value);
            Schema.Templet.StopReason.ID = "";
            Schema.Templet.StopReason.Name = "";
            Schema.Templet.Stop.ID = ZA4.Get<NHapi.Model.V24.Datatype.ST>(23).Value; ;
            if (ZA4.Get<NHapi.Model.V24.Datatype.TS>(24).TimeOfAnEvent.Value != null)
            Schema.Templet.Stop.OperTime = DateTime.ParseExact(ZA4.Get<NHapi.Model.V24.Datatype.TS>(24).TimeOfAnEvent.Value, "yyyyMMddHHmmss",null);
            Schema.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(ZA4.Get<NHapi.Model.V24.Datatype.NM>(25).Value);
            Schema.Templet.RegLevel.ID = ZA4.Get<NHapi.Model.V24.Datatype.ST>(26).Value;
            Schema.Templet.RegLevel.Name = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetRegLevelName(ZA4.Get<NHapi.Model.V24.Datatype.ST>(26).Value);//挂号级别
            Schema.Templet.Memo = ZA4.Get<NHapi.Model.V24.Datatype.ST>(28).Value;
            Schema.Templet.Oper.ID = ZA4.Get<NHapi.Model.V24.Datatype.ST>(29).Value;
            if (ZA4.Get<NHapi.Model.V24.Datatype.TS>(30).TimeOfAnEvent.Value != null)
                Schema.Templet.Oper.OperTime = DateTime.ParseExact(ZA4.Get<NHapi.Model.V24.Datatype.TS>(30).TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
            else
                Schema.Templet.Oper.OperTime = DateTime.Now;
            Schema.Templet.Noon.ID = ZA4.Get<NHapi.Model.V24.Datatype.ST>(31).Value;

                //MFE
            NHapi.Model.V24.Group.MFN_M01_MF MF = MFN.GetMF(0);
            if( MF.MFE.RecordLevelEventCode.Value == "MAD") //排班
            {
                if (schMgr.Insert(Schema) == -1)
                {
                    errInfo = schMgr.Err;
                    return -1;
                }
            }
            else if(MF.MFE.RecordLevelEventCode.Value =="MUP")  //排班修改
            {
                int i = schMgr.Update(Schema);
                if (i == -1)
                {
                    errInfo = schMgr.Err;
                    return -1;
                }
                else if (i == 0)
                {
                    if(schMgr.Insert(Schema) == -1)
                    {
                        errInfo = schMgr.Err;
                        return -1;
                    }
                }
            }
            else if(MF.MFE.RecordLevelEventCode.Value =="MDL") //删除
            {
                if(schMgr.Delete(Schema.Templet.ID) == -1)
                {
                  errInfo =schMgr.Err;
                    return -1;
                }
            
            }

            return 1;


        }

    }
}
