using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A31
{
    /// <summary>
    /// 住院医生下诊断
    /// </summary>
    public class InPatientDiagnose
    {
        public int ProcessMessage(NHapi.Model.V24.Message.ADT_A31 adtA05, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagnoseMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            FS.HISFC.BizLogic.RADT.InPatient inpatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
            diagnoseMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            inpatientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            string inpatientNO = adtA05.PV1.VisitNumber.ID.Value;
            FS.HISFC.Models.RADT.PatientInfo patientInfo = inpatientMgr.QueryPatientInfoByInpatientNO(inpatientNO);
            if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                errInfo = "查找住院病人信息失败，流水号：" + inpatientNO;
                return -1;
            }

            FS.HISFC.Models.HealthRecord.Diagnose diagnose = null;
            for (int i = 0; i < adtA05.DG1RepetitionsUsed; i++)
            {
                diagnose = new FS.HISFC.Models.HealthRecord.Diagnose();

                diagnose.DiagInfo.Patient.ID = inpatientNO;//流水号
                //是否主诊断 1 主诊断 0 其他诊断  
                diagnose.DiagInfo.IsMain = adtA05.GetDG1(i).DiagnosisPriority.Value == "1";
                if (diagnose.DiagInfo.IsMain)
                {
                    diagnose.DiagInfo.DiagType.ID = ((int)FS.HISFC.Models.Base.EnumDiagnoseType.OUT).ToString();//出院诊断
                }
                else
                {
                    diagnose.DiagInfo.DiagType.ID = ((int)FS.HISFC.Models.Base.EnumDiagnoseType.OTHER).ToString();//其他诊断
                }

                //发生序号
                diagnose.DiagInfo.HappenNo = 1;

                // 诊断类型
                diagnose.DiagInfo.DiagType.ID = ((int)FS.HISFC.Models.Base.EnumDiagnoseType.CLINIC).ToString();
                // 诊断ICD码
                diagnose.DiagInfo.ICD10.ID = adtA05.GetDG1(i).DiagnosisCodeDG1.Identifier.Value;
                if (string.IsNullOrEmpty(diagnose.DiagInfo.ICD10.ID))
                {
                    diagnose.DiagInfo.ICD10.ID = "MS999";
                }
                // 诊断名称
                diagnose.DiagInfo.ICD10.Name = adtA05.GetDG1(i).DiagnosisDescription.Value;
                // 诊断日期
                diagnose.DiagInfo.DiagDate = DateTime.ParseExact(adtA05.EVN.RecordedDateTime.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                // 医师信息
                diagnose.DiagInfo.Doctor.ID = adtA05.EVN.GetOperatorID(0).IDNumber.Value;
                diagnose.DiagInfo.Doctor.Name = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetEmployeeName(diagnose.DiagInfo.Doctor.ID);

                diagnose.DiagInfo.Dept.Name = CommonController.CreateInstance().GetDepartmentName(diagnose.DiagInfo.Dept.ID);
                //有效性
                diagnose.IsValid = true;
                //操作时间和操作人
                diagnose.OperInfo.ID = adtA05.EVN.GetOperatorID(0).IDNumber.Value;//操作员

                diagnose.PerssonType = FS.HISFC.Models.Base.ServiceTypes.I;
                diagnose.OperType = "1";//医生录入

                int j = diagnoseMgr.UpdateDiagnose(diagnose);
                if (j == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "诊断：" + diagnose.DiagInfo.ICD10.Name + "保存失败:" + diagnoseMgr.Err;
                    return -1;
                }
                else if (j == 0)
                {
                    if (diagnoseMgr.InsertDiagnose(diagnose) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = "诊断：" + diagnose.DiagInfo.ICD10.Name + "保存失败:" + diagnoseMgr.Err;
                        return -1;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

    }
}
