using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01
{
    public class Department
    {
        /// <summary>
        /// 将HIS的科室信息实体转换成HL7的科室实体
        /// 目前使用Z段
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        private NHapi.Model.V24.Message.MFN_M01 ConvertToHL7Object(FS.HISFC.Models.Base.Department dept, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType)
        {
            NHapi.Model.V24.Message.MFN_M01 HL7Dept = new NHapi.Model.V24.Message.MFN_M01();
            FS.HL7Message.V24.Function.GenerateMSH(HL7Dept.MSH);

            //MSH
            HL7Dept.MSH.MessageType.MessageType.Value = "MFN";
            HL7Dept.MSH.MessageType.TriggerEvent.Value = "M01";

            //MFI
            //主文件标识符
            HL7Dept.MFI.MasterFileIdentifier.Identifier.Value = "Department";
            HL7Dept.MFI.MasterFileIdentifier.Text.Value = "科室";
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
             *  科室编码
                科室名称
                科室英文
                科室简称
                科室类型
                是否挂号科室 
                是否核算科室 
                特殊科室属性 
                有效性标志 
                顺序号
                操作员
                操作时间
             */
            //Z段字段名称
            MF.Zxx.Name = "Z01";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(4, "").Value = dept.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(30, "").Value = dept.Name;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = dept.EnglishName;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = dept.ShortName;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(4, "").Value = dept.DeptType.ID.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = dept.IsRegDept ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = dept.IsStatDept ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = dept.SpecialFlag;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = (dept.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid) ? "1" : "0";

            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = dept.SortID.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(8, "").Value = dept.SpellCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(8, "").Value = dept.WBCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = FS.FrameWork.Management.Connection.Operator.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");



            return HL7Dept;
        }

        public int Send(System.Collections.ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType,ref List<NHapi.Model.V24.Message.MFN_M01> listMFN_M01, ref string errInfo)
        {
            if (alInfo == null)
            {
                errInfo = "包含科室信息的数组为null";
                return -1;
            }
            if (alInfo.Count == 0)
            {
                errInfo = "包含科室信息的数组没有元素";
                return 0;
            }

            foreach (FS.HISFC.Models.Base.Department dept in alInfo)
            {
                NHapi.Model.V24.Message.MFN_M01 HL7Dept = this.ConvertToHL7Object(dept,operType);
                listMFN_M01.Add(HL7Dept);
            }
            return alInfo.Count;
        }


        public int Receive(NHapi.Model.V24.Message.MFN_M01 MFN, ref string errInfo)
        {
            FS.HISFC.BizLogic.Manager.Department departmentMgr = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.Models.Base.Department department = new FS.HISFC.Models.Base.Department();

            NHapi.Model.V24.Segment.Z01 ZA1 = MFN.GetMF(0).GetStructure<NHapi.Model.V24.Segment.Z01>();
            department.ID  = ZA1.Get<NHapi.Model.V24.Datatype.ST>(1).Value;
            department.Name = ZA1.Get<NHapi.Model.V24.Datatype.ST>(2).Value;
            department.EnglishName = ZA1.Get<NHapi.Model.V24.Datatype.ST>(3).Value;
            department.ShortName = ZA1.Get<NHapi.Model.V24.Datatype.ST>(4).Value;
            department.DeptType.ID = ZA1.Get<NHapi.Model.V24.Datatype.ST>(5).Value;
            department.IsRegDept = FS.FrameWork.Function.NConvert.ToBoolean(ZA1.Get<NHapi.Model.V24.Datatype.ST>(6).Value);
            department.IsStatDept = FS.FrameWork.Function.NConvert.ToBoolean(ZA1.Get<NHapi.Model.V24.Datatype.ST>(7).Value);
            department.SpecialFlag = ZA1.Get<NHapi.Model.V24.Datatype.ST>(8).Value;
            department.ValidState =  (ZA1.Get<NHapi.Model.V24.Datatype.ST>(9).Value == "1")? FS.HISFC.Models.Base.EnumValidState.Valid:FS.HISFC.Models.Base.EnumValidState.Invalid;
            if (ZA1.Get<NHapi.Model.V24.Datatype.ST>(9).Value == "2")
            {
                department.ValidState = FS.HISFC.Models.Base.EnumValidState.Ignore;
            }
            department.SortID = FS.FrameWork.Function.NConvert.ToInt32(ZA1.Get<NHapi.Model.V24.Datatype.ST>(10).Value);
            department.SpellCode = ZA1.Get<NHapi.Model.V24.Datatype.ST>(11).Value;
            department.WBCode = ZA1.Get<NHapi.Model.V24.Datatype.ST>(12).Value;

            int param = departmentMgr.Delete(department.ID);
            if (param <0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = departmentMgr.Err;
                return -1;
            }

            param = departmentMgr.Insert(department);
            if (param <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = departmentMgr.Err;
                return -1;
            }


            return 1;
        }

    }
}
