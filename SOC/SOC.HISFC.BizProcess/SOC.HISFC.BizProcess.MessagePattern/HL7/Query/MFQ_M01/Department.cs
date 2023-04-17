using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.MFQ_M01
{
    public class Department 
    {
        private int processMessage(NHapi.Model.V24.Message.MFQ_M01 o, ref NHapi.Model.V24.Message.MFR_M01 mfrM01, ref string errInfo)
        {

            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

            ArrayList alDept = deptMgr.GetRegDepartment();
            if (alDept == null)
            {
                errInfo = deptMgr.Err;
                return -1;
            }

            #region QRD

            mfrM01.QRD.QueryID.Value = o.QRD.QueryID.Value;

            #endregion

            #region MFI

            //MFI
            mfrM01.MFI.MasterFileIdentifier.Identifier.Value = "Department";
            mfrM01.MFI.MasterFileIdentifier.Text.Value = "科室信息";
            mfrM01.MFI.MasterFileIdentifier.NameOfCodingSystem.Value = "HIS";
            //文件层事件代码
            mfrM01.MFI.FileLevelEventCode.Value = "UPD";
            mfrM01.MFI.EnteredDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            mfrM01.MFI.EffectiveDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            //应答层代码
            mfrM01.MFI.ResponseLevelCode.Value = "AL";

            #endregion

            for (int i = 0; i < alDept.Count; i++)
            {
                #region MFE

                FS.HISFC.Models.Base.Department dept = alDept[i] as FS.HISFC.Models.Base.Department;
                //MFE
                NHapi.Model.V24.Group.MFR_M01_MF_QUERY MF = mfrM01.GetMF_QUERY(mfrM01.MF_QUERYRepetitionsUsed);
                NHapi.Base.Model.Varies primaryKeyValue = MF.MFE.GetPrimaryKeyValueMFE(0);
                ((NHapi.Base.Model.GenericPrimitive)primaryKeyValue.Data).Value = dept.ID;
                NHapi.Model.V24.Datatype.ID primaryKeyValueType = MF.MFE.GetPrimaryKeyValueType(0);
                primaryKeyValueType.Value = "CE";//编码元素

                #endregion

                #region Z15

                //Z段字段名称
                MF.Zxx.Name = "Z15";
                MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(10, "").Value = "A2_B1";
                MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = dept.ID;
                MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(100, "").Value = dept.Name;
                MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = "0";
                MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = "0";
                MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = "0";
                MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = "0";

                #endregion
            }

            return 1;
        }

        public  int ProcessMessage(NHapi.Model.V24.Message.MFQ_M01 o, ref NHapi.Model.V24.Message.MFR_M01 ackMessage, ref string errInfo)
        {
            return this.processMessage(o, ref ackMessage,ref errInfo);
        }

    }
}
