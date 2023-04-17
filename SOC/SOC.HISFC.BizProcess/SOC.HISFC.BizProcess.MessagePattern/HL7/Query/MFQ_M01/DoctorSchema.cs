using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.MFQ_M01
{
    public class DoctorSchema
    {
        private int processMessage(NHapi.Model.V24.Message.MFQ_M01 o, ref NHapi.Model.V24.Message.MFR_M01 mfrM01, ref string errInfo)
        {

            string deptID = o.QRD.GetWhatDepartmentDataCode(0).Identifier.Value;//科室编码
            if (string.IsNullOrEmpty(deptID))
            {
                errInfo = "科室编码为空，MFQ_M01-QRD-10";
                return -1;
            }
            string doctorType = o.QRD.GetWhatDataCodeValueQual(0).FirstDataCodeValue.Value;//1表示普通医生，2表示专家
            if (string.IsNullOrEmpty(doctorType))
            {
                errInfo = "医生类型为空，MFQ_M01-QRD-11";
                return -1;
            }

            FS.HISFC.BizLogic.Registration.Schema schemaMgr = new FS.HISFC.BizLogic.Registration.Schema(); 
            FS.HISFC.BizLogic.Registration.RegLvlFee regLevelFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();
            ArrayList alDoctor = schemaMgr.Query(CommonController.CreateInstance().GetSystemTime().Date, FS.HISFC.Models.Base.EnumSchemaType.Doct, deptID);
            if (alDoctor == null)
            {
                errInfo = schemaMgr.Err;
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

            for (int i = 0; i < alDoctor.Count; i++)
            {
                #region MFE

                FS.HISFC.Models.Registration.Schema schema = alDoctor[i] as FS.HISFC.Models.Registration.Schema;
                //查找挂号级别和挂号费
                FS.HISFC.Models.Registration.RegLvlFee regLvlFee= regLevelFeeMgr.Get("1", schema.Templet.RegLevel.ID);

                //只需要专家或者不是专家的
                if (FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetRegLevel(regLvlFee.RegLevel.ID) != null)
                {
                    regLvlFee.RegLevel = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetRegLevel(regLvlFee.RegLevel.ID);

                    if (doctorType.Equals("1"))
                    {
                        if (regLvlFee.RegLevel.IsExpert)
                        {
                            continue;
                        }
                    }
                    else if (doctorType.Equals("2"))
                    {
                        if (regLvlFee.RegLevel.IsExpert == false)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }

                //MFE
                NHapi.Model.V24.Group.MFR_M01_MF_QUERY MF = mfrM01.GetMF_QUERY(mfrM01.MF_QUERYRepetitionsUsed);
                NHapi.Base.Model.Varies primaryKeyValue = MF.MFE.GetPrimaryKeyValueMFE(0);
                ((NHapi.Base.Model.GenericPrimitive)primaryKeyValue.Data).Value = schema.Templet.Doct.ID;
                NHapi.Model.V24.Datatype.ID primaryKeyValueType = MF.MFE.GetPrimaryKeyValueType(0);
                primaryKeyValueType.Value = "CE";//编码元素

                #endregion

                #region Z15

                //Z段字段名称
                MF.Zxx.Name = "Z15";
                MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(10, "").Value = "A2_B1";
                MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = schema.Templet.Doct.ID;
                MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(100, "").Value = schema.Templet.Doct.Name;
                MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = regLvlFee.RegFee.ToString("F2");
                MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = regLvlFee.OwnDigFee.ToString("F2");
                MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = schema.RegedQTY.ToString();
                MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = schema.Templet.RegQuota.ToString();

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
