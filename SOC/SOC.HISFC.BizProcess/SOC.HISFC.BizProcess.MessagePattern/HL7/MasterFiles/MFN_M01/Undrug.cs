using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01
{
    public class Undrug
    {
        private NHapi.Model.V24.Message.MFN_M01 ConvertToHL7Object(FS.HISFC.Models.Fee.Item.Undrug undrug, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType)
        {
            ArrayList alLABSAMPLE = new ArrayList();
            
           alLABSAMPLE = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().QueryConstant("LABSAMPLE");
            NHapi.Model.V24.Message.MFN_M01 HL7Undrug = new NHapi.Model.V24.Message.MFN_M01();
            FS.HL7Message.V24.Function.GenerateMSH(HL7Undrug.MSH);

            //MSH
            HL7Undrug.MSH.MessageType.MessageType.Value = "MFN";
            HL7Undrug.MSH.MessageType.TriggerEvent.Value = "M01";

            //MFI
            //主文件标识符
            HL7Undrug.MFI.MasterFileIdentifier.Identifier.Value = "Undrug";
            HL7Undrug.MFI.MasterFileIdentifier.Text.Value = "非药品物价信息";
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
            else if (operType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Valid)
            {
                MF.MFE.RecordLevelEventCode.Value = "MDC";
            }
            else if (operType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.InValid)
            {
                MF.MFE.RecordLevelEventCode.Value = "MAC";
            }

            NHapi.Base.Model.Varies primaryKeyValue = MF.MFE.GetPrimaryKeyValueMFE(0);
            ((NHapi.Base.Model.GenericPrimitive)primaryKeyValue.Data).Value = undrug.ID;

            NHapi.Model.V24.Datatype.ID primaryKeyValueType = MF.MFE.GetPrimaryKeyValueType(0);
            primaryKeyValueType.Value = "CE";//编码元素

            /*  Z段包含内容
                非药品编码	            ST	R	12
                用户自定义（助记）码	ST	O	20
                非药品名称	            ST	O	100
                系统类别	            ST	O	3
                最小费用代码	        ST	O	3
                国家编码	            ST	O	16
                国际标准代码	        ST	O	20
                三甲价	                NM	O	12
                单位	                ST	O	30
                特定治疗项目    0假 1真	ST	O	1
                计划生育标记	        ST	O	1
                确认标志	            ST	O	1
                有效性标识	            ST	O	1
                规格	                ST	O	32
                执行科室	            ST	O	200
                单位标识	            ST	O	1
                备注	                ST	O	2000
                操作员	                ST	O	6
                操作日期	            TS	O	26

             */
            //Z段字段名称
            MF.Zxx.Name = "Z04";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(12, "").Value = undrug.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = undrug.UserCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(100, "").Value = undrug.Name;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(3, "").Value = undrug.SysClass.ID.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(3, "").Value = undrug.MinFee.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = undrug.GBCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = undrug.NationCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(12, "").Value = undrug.Price.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(30, "").Value = undrug.PriceUnit;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = undrug.SpecialFlag3;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = undrug.IsFamilyPlanning ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = undrug.IsNeedConfirm ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = undrug.ValidState;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(32, "").Value = undrug.Specs;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(200, "").Value = undrug.ExecDept;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = undrug.UnitFlag;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(2000, "").Value = undrug.Memo;
            if(undrug.ApplicabilityArea ==""||undrug.ApplicabilityArea ==null)
            {
                undrug.ApplicabilityArea ="0"; //默认为全院
            }
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(20, "").Value = undrug.ApplicabilityArea;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(8, "").Value = undrug.SpellCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(8, "").Value = undrug.WBCode;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = FS.FrameWork.Management.Connection.Operator.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(50, "").Value = undrug.CheckApplyDept;
            if (undrug.SysClass.ID.Equals(FS.HISFC.Models.Base.EnumSysClass.UL.ToString()))
            {
                foreach(FS.HISFC.Models.Base.Const LABSAMPLE in alLABSAMPLE)
                {
                    if (LABSAMPLE.Name == undrug.CheckBody)
                    {
                        MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(50, "").Value = LABSAMPLE.ID;
                        break;
                    }
                  
                }

            }
            else
            {
                MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(50, "").Value = "";
            }

            //打包收费标记
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(5, "").Value = undrug.SpecialFlag4;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(5, "").Value = undrug.SpecialFlag2; //限制标志
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(5, "").Value = undrug.SpecialFlag1;//住院打包标志
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(5, "").Value = undrug.SpecialFlag;//开立不显示

            return HL7Undrug;
        }

        public int Send(System.Collections.ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType, ref List<NHapi.Model.V24.Message.MFN_M01> listMFN_M01, ref string errInfo)
        {
            if (alInfo == null)
            {
                errInfo = "包含非药品信息的数组为null";
                return -1;
            }
            if (alInfo.Count == 0)
            {
                errInfo = "包含非药品信息的数组没有元素";
                return 0;
            }

            foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in alInfo)
            {
                NHapi.Model.V24.Message.MFN_M01 HL7Undrug = this.ConvertToHL7Object(undrug, operType);
                listMFN_M01.Add(HL7Undrug);
            }
            return alInfo.Count;
        }

    }
}
