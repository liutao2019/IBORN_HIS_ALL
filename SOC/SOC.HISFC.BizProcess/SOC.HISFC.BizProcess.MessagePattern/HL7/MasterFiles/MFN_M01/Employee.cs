using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01
{
    public class Employee
    {
        private NHapi.Model.V24.Message.MFN_M01 ConvertToHL7Object(FS.HISFC.Models.Base.Employee employee, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType)
        {
            NHapi.Model.V24.Message.MFN_M01 HL7Employee = new NHapi.Model.V24.Message.MFN_M01();
            FS.HL7Message.V24.Function.GenerateMSH(HL7Employee.MSH);

            //MSH
            HL7Employee.MSH.MessageType.MessageType.Value = "MFN";
            HL7Employee.MSH.MessageType.TriggerEvent.Value = "M01";

            //MFI
            //主文件标识符
            HL7Employee.MFI.MasterFileIdentifier.Identifier.Value = "Employee";
            HL7Employee.MFI.MasterFileIdentifier.Text.Value = "人员";
            HL7Employee.MFI.MasterFileIdentifier.NameOfCodingSystem.Value = "HIS";

            //文件层事件代码
            HL7Employee.MFI.FileLevelEventCode.Value = "UPD";
            HL7Employee.MFI.EnteredDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            HL7Employee.MFI.EffectiveDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");

            //应答层代码
            HL7Employee.MFI.ResponseLevelCode.Value = "AL";

            //MFE
            NHapi.Model.V24.Group.MFN_M01_MF MF = HL7Employee.GetMF(0);

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
            ((NHapi.Base.Model.GenericPrimitive)primaryKeyValue.Data).Value = employee.ID;

            NHapi.Model.V24.Datatype.ID primaryKeyValueType = MF.MFE.GetPrimaryKeyValueType(0);
            primaryKeyValueType.Value = "CE";//编码元素

            /*  Z段包含内容
            员工代码	ST	R	6
            员工姓名	ST	O	10
            性别	    ST	O	1
            出生日期	TS	O	26
            职务代号	ST	O	2
            职级代号	ST	O	2
            学历	    ST	O	2
            身份证号	ST	O	18
            所属科室号	ST	O	4
            所属护理站	ST	O	4
            人员类型	ST	O	2
            是否专家	ST	O	1
            有效性标志 	ST	O	1
            顺序号	    NM	O	4
            操作员	    ST	O	6
            操作时间	TS	O	26

             */
            //Z段字段名称
            MF.Zxx.Name = "Z02";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = employee.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(10, "").Value = employee.Name;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = employee.Sex.ID.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = employee.Birthday.ToString("yyyyMMddHHmmss");
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(2, "").Value = employee.Duty.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(2, "").Value = employee.Level.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(2, "").Value = employee.Expert.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(18, "").Value = employee.IDCard;
            NHapi.Model.V24.Datatype.CE CE = MF.Zxx.Add<NHapi.Model.V24.Datatype.CE>(4, "");
            CE.Identifier.Value = employee.Dept.ID;
            CE.Text.Value = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(employee.Dept.ID);

            //MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(50, "").Value = employee.Dept.Name;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(4, "").Value = employee.Nurse.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(4, "").Value = employee.EmployeeType.ID.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(4, "").Value = employee.IsExpert ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = (employee.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid) ? "1" : "0";

            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = employee.SortID.ToString();

            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(8, "").Value = employee.SpellCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(8, "").Value = employee.WBCode;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = FS.FrameWork.Management.Connection.Operator.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");

            return HL7Employee;
        }

        public int Send(System.Collections.ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType, ref List<NHapi.Model.V24.Message.MFN_M01> listMFN_M01, ref string errInfo)
        {
            if (alInfo == null)
            {
                errInfo = "包含人员信息的数组为null";
                return -1;
            }
            if (alInfo.Count == 0)
            {
                errInfo = "包含人员信息的数组没有元素";
                return 0;
            }

            foreach (FS.HISFC.Models.Base.Employee employee in alInfo)
            {
                NHapi.Model.V24.Message.MFN_M01 HL7Employee = this.ConvertToHL7Object(employee, operType);
                listMFN_M01.Add(HL7Employee);
            }
            return alInfo.Count;
        }

    }
}
