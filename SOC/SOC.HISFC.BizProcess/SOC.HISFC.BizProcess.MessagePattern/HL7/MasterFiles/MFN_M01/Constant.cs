using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01
{
    public class Constant
    {
        private NHapi.Model.V24.Message.MFN_M01 ConvertToHL7Object(FS.HISFC.Models.Base.Const constant, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType)
        {
            NHapi.Model.V24.Message.MFN_M01 HL7Constant = new NHapi.Model.V24.Message.MFN_M01();
            FS.HL7Message.V24.Function.GenerateMSH(HL7Constant.MSH);

            //MSH
            HL7Constant.MSH.MessageType.MessageType.Value = "MFN";
            HL7Constant.MSH.MessageType.TriggerEvent.Value = "M01";

            //MFI
            //主文件标识符
            HL7Constant.MFI.MasterFileIdentifier.Identifier.Value = constant.OperEnvironment.Memo;
            HL7Constant.MFI.MasterFileIdentifier.Text.Value = "通用常数字典信息";
            HL7Constant.MFI.MasterFileIdentifier.NameOfCodingSystem.Value = "HIS";

            //文件层事件代码
            HL7Constant.MFI.FileLevelEventCode.Value = "UPD";
            HL7Constant.MFI.EnteredDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            HL7Constant.MFI.EffectiveDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");

            //应答层代码
            HL7Constant.MFI.ResponseLevelCode.Value = "AL";

            //MFE
            NHapi.Model.V24.Group.MFN_M01_MF MF = HL7Constant.GetMF(0);

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
            ((NHapi.Base.Model.GenericPrimitive)primaryKeyValue.Data).Value = constant.ID;

            NHapi.Model.V24.Datatype.ID primaryKeyValueType = MF.MFE.GetPrimaryKeyValueType(0);
            primaryKeyValueType.Value = "CE";//编码元素

            /*  Z段包含内容
            类型	ST	R	20
            编码	ST	O	20
            名称	ST	O	1000
            用户自定义助记码	ST	O	30
            顺序号	NM	O	4
            有效性标志 	ST	O	1
            备注	ST	O	2000
            操作员	ST	O	6
            操作时间	TS	O	26
             */
            //Z段字段名称
            MF.Zxx.Name = "Z99";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = constant.OperEnvironment.Memo;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = constant.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1000, "").Value = constant.Name;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(30, "").Value = constant.UserCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = constant.SortID.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = constant.IsValid ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(2000, "").Value = constant.Memo;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(30, "").Value = constant.SpellCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(30, "").Value = constant.WBCode;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = constant.OperEnvironment.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = constant.OperEnvironment.OperTime.ToString("yyyyMMddHHmmss");



            return HL7Constant;
        }

        public int Send(System.Collections.ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType, ref List<NHapi.Model.V24.Message.MFN_M01> listMFN_M01, ref string errInfo)
        {
            if (alInfo == null)
            {
                errInfo = "包含常数信息的数组为null";
                return -1;
            }
            if (alInfo.Count == 0)
            {
                errInfo = "包含常数信息的数组没有元素";
                return 0;
            }

            foreach (FS.HISFC.Models.Base.Const constant in alInfo)
            {
                if (operType == FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.AddAfterDelete)
                {
                    NHapi.Model.V24.Message.MFN_M01 HL7Constant = this.ConvertToHL7Object(constant, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete);
                    listMFN_M01.Add(HL7Constant);

                    HL7Constant = this.ConvertToHL7Object(constant, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add);
                    listMFN_M01.Add(HL7Constant);
                }
                else
                {
                    NHapi.Model.V24.Message.MFN_M01 HL7Constant = this.ConvertToHL7Object(constant, operType);
                    listMFN_M01.Add(HL7Constant);
                }
            }
            return listMFN_M01.Count;
        }

        public int Receive(NHapi.Model.V24.Message.MFN_M01 MFN, ref string errInfo)
        {
            /*  1	6	ST	R	code	编码	
                2	60	ST	R	name	名称	
                3	8	ST	O	py_code	拼音码	
                4	8	ST	O	d_code	自定义码	
                5	1	ST	O	class_code	分类	"   1 口服
                                                        2 输液
                                                        3 注射
                                                        4 出院带药
                                                        5 领药(临时出现)
                                                        6 退药
                                                        7 领药(不出现)
                                                        9 其他
                                                        a 草药"
                6	1	ST	O	sort_code	排序码	
                7	1	ST	O	print_name	打印名称	
                8	1	ST	O	doc_used	删除标记	1 删除 0正常
                */
            FS.HISFC.Models.Base.Const constant = new FS.HISFC.Models.Base.Const();
            NHapi.Model.V24.Segment.ZA2 ZA2 = MFN.GetMF().GetStructure<NHapi.Model.V24.Segment.ZA2>();
            constant.ID = ZA2.Get<NHapi.Model.V24.Datatype.ST>(1).Value;
            constant.Name = ZA2.Get<NHapi.Model.V24.Datatype.ST>(2).Value;
            constant.SpellCode = ZA2.Get<NHapi.Model.V24.Datatype.ST>(3).Value;
            constant.WBCode = ZA2.Get<NHapi.Model.V24.Datatype.ST>(4).Value;
            //MFE
            NHapi.Model.V24.Group.MFN_M01_MF MF = MFN.GetMF(0);

            switch (ZA2.Get<NHapi.Model.V24.Datatype.ST>(5).Value)
            {
                case "1":
                    constant.UserCode = "PO";
                    break;
                case "2":
                    constant.UserCode = "IO";
                    break;
                case "3":
                    constant.UserCode = "IO";
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
                    break;
                case "7":
                    break;
                case "8":
                    break;
                case "9":
                    constant.UserCode = "O1";
                    break;
                case "a":
                    break;
                default:
                    break;
            }

            int sortNum = 0;
            if (int.TryParse(ZA2.Get<NHapi.Model.V24.Datatype.ST>(6).Value, out sortNum))
            {
                constant.SortID = sortNum;
            }
            constant.Memo = ZA2.Get<NHapi.Model.V24.Datatype.ST>(7).Value;
            constant.IsValid = (ZA2.Get<NHapi.Model.V24.Datatype.ST>(8).Value == "0");

            FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            int param = constantMgr.InsertItem("USAGE", constant);
            if (param != 1)
            {
                if (string.IsNullOrEmpty(constant.UserCode))
                {
                    FS.HISFC.Models.Base.Const curConstant = (FS.HISFC.Models.Base.Const)constantMgr.GetConstant("USAGE", constant.ID);
                    constant.UserCode = curConstant.UserCode;
                }
                param = constantMgr.UpdateItem("USAGE", constant);
                if (param != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = constantMgr.Err;
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            return param;
        }
    }
}

