using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A10
{
    /// <summary>
    /// 门诊到诊
    /// </summary>
    public class OutPatientArrive
    {
        public int ProcessMessage(NHapi.Model.V24.Message.ADT_A10 adtA10, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            //将分诊队列更新成5
            string clinicNO = adtA10.PV1.VisitNumber.ID.Value;
            if (string.IsNullOrEmpty(clinicNO))
            {
                errInfo = "接诊失败，原因：患者流水号为空";
                return -1;
            }

            string doctID = adtA10.PV1.GetAttendingDoctor(0).IDNumber.Value;

            //FS.HISFC.BizLogic.Nurse.Assign assignManger = new FS.HISFC.BizLogic.Nurse.Assign();
            FS.SOC.HISFC.Assign.BizLogic.Assign assignManger = new FS.SOC.HISFC.Assign.BizLogic.Assign();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            assignManger.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (assignManger.Update(FS.HISFC.Models.Nurse.EnuTriageStatus.Arrive,clinicNO,null,null, doctID) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "接诊失败，原因：" + assignManger.Err;
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }

    }
}
