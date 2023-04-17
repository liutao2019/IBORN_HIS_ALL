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
    /// 门诊医生下诊断
    /// </summary>
    public class OutPatientDiagnose 
    {
        
        public int ProcessMessage(NHapi.Model.V24.Message.ADT_A31 adtA05, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagnoseMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

            diagnoseMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
           
            //获取患者挂号信息
            FS.HISFC.Models.Registration.Register regInfo = new FS.HISFC.Models.Registration.Register();
            string clinicCode = adtA05.PV1.VisitNumber.ID.Value;
            regInfo = regMgr.GetByClinic(clinicCode);
            if (regInfo == null || string.IsNullOrEmpty(regInfo.ID))
            {
                errInfo = "获取患者信息" + clinicCode + "失败!" + regMgr.Err;
                return -1;
            }
            if (regInfo.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
            {
                errInfo = "获取挂号信息失败，原因：挂号记录已退号" + regInfo.ID;
                return -1;
            }
            else if (regInfo.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
            {
                errInfo = "获取挂号信息失败，原因：挂号记录已作废" + regInfo.ID;
                return -1;
            }


            FS.HISFC.Models.HealthRecord.Diagnose diagnose = null;
            for (int i = 0; i < adtA05.DG1RepetitionsUsed; i++)
            {
                diagnose = new FS.HISFC.Models.HealthRecord.Diagnose();

                //患者信息
                diagnose.DiagInfo.Patient.ID = regInfo.ID;
                diagnose.DiagInfo.Patient.PID.CardNO = regInfo.PID.CardNO;

                if (adtA05.GetDG1(i).SetIDDG1.Value != null)
                {
                    //发生序号
                    diagnose.DiagInfo.HappenNo = FS.FrameWork.Function.NConvert.ToInt32(adtA05.GetDG1(i).SetIDDG1.Value); //修改为保存多个诊断
                }

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

                //科室信息
                if (string.IsNullOrEmpty(regInfo.SeeDoct.Dept.ID))
                {
                    diagnose.DiagInfo.Dept.ID = regInfo.DoctorInfo.Templet.Dept.ID;
                }
                else
                {
                    diagnose.DiagInfo.Dept.ID = regInfo.SeeDoct.Dept.ID;
                }
                diagnose.DiagInfo.Dept.Name = CommonController.CreateInstance().GetDepartmentName(diagnose.DiagInfo.Dept.ID);
                //有效性
                diagnose.IsValid = true;
                //操作时间和操作人
                diagnose.OperInfo.ID = adtA05.EVN.GetOperatorID(0).IDNumber.Value;//操作员

                //是否主诊断 1 主诊断 0 其他诊断  
                diagnose.DiagInfo.IsMain = adtA05.GetDG1(i).DiagnosisPriority.Value == "1";

                diagnose.PerssonType = FS.HISFC.Models.Base.ServiceTypes.C;
                diagnose.OperType = "1";//医生录入

                if (adtA05.GetDG1(i).DiagnosisCodingMethod.Value == "MUP")
                {

                    int j = diagnoseMgr.UpdateDiagnose(diagnose);
                    if (j == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = "诊断：" + diagnose.DiagInfo.ICD10.Name + "保存失败:" + diagnoseMgr.Err;
                        return -1;
                    }
                }
                if (adtA05.GetDG1(i).DiagnosisCodingMethod.Value == "CA")
                {
                   // int j = diagnoseMgr.DeleteDiagnoseSingle(regInfo.ID, diagnose.DiagInfo.HappenNo);
                    //chenxin 用MET_cas_DIAGNOSE表
                    int j = diagnoseMgr.DeleteDiagnoseSingleForClinic(regInfo.ID, diagnose.DiagInfo.ICD10.ID, diagnose.DiagInfo.HappenNo.ToString());    
                    if (j == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = "诊断：" + diagnose.DiagInfo.ICD10.Name + "保存失败:" + diagnoseMgr.Err;
                        return -1;
                    }
                
                }
                if (adtA05.GetDG1(i).DiagnosisCodingMethod.Value == "NW")
                {
                    if (diagnoseMgr.InsertCasDiagnose(diagnose) < 0)
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
