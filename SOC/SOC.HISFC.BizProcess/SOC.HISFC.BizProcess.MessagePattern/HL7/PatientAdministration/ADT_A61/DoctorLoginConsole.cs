using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A61
{
    /// <summary>
    /// 医生登录诊台
    /// </summary>
    public class DoctorLoginConsole
    {
        public int ProcessMessage(NHapi.Model.V24.Message.ADT_A61 adtA61, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            //取医生，取登录的诊台
            string consoleID = adtA61.PV1.AssignedPatientLocation.Bed.Value;
            if (string.IsNullOrEmpty(consoleID))
            {
                errInfo = "医生登录诊台处理失败，原因：诊台编码为空";
                return -1;
            }

            string doctorID = adtA61.PV1.GetAttendingDoctor(0).IDNumber.Value;
            if (string.IsNullOrEmpty(doctorID))
            {
                errInfo = "医生登录诊台处理失败，原因：医生编码为空";
                return -1;
            }
            FS.HISFC.Models.Base.Employee employee = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetEmployee(doctorID);
            if (employee == null)
            {
                errInfo = "医生登录诊台处理失败，原因：传入的医生编码，系统中找不到" + doctorID;
                return -1;
            }


            //FS.SOC.HISFC.BizLogic.Nurse.Seat seatManager = new FS.SOC.HISFC.BizLogic.Nurse.Seat();
            //if (seatManager.UpdateDoctInfo(consoleID, employee.ID) == -1)
            //{
            //    errInfo = "医生登录诊台处理失败，原因：" + seatManager.Err; 
            //    return -1;
            //}

            return 1;
        }
    }
}
