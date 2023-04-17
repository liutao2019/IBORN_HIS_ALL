using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A10
{
    /// <summary>
    /// 住院接诊
    /// </summary>
    public class InPatientArrive
    {
        FS.HISFC.BizProcess.Integrate.RADT RADTIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        public int ProcessMessage(NHapi.Model.V24.Message.ADT_A10 adtA10, ref NHapi.Base.Model.IMessage ackMessage,ref string errInfo)
        {
            if (string.IsNullOrEmpty(adtA10.PV1.VisitNumber.ID.Value))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "接诊失败，原因：患者流水号为空";
                return -1;
            }

            FS.HISFC.Models.RADT.PatientInfo patientInfo = RADTIntegrate.QueryPatientInfoByInpatientNO(adtA10.PV1.VisitNumber.ID.Value);
            if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "接诊获取" + adtA10.PV1.VisitNumber.ID.Value + "患者信息失败，原因：" + this.RADTIntegrate.Err;
                return -1;
            }
            //取处理时的床位信息
            FS.HISFC.Models.Base.Bed bed = new FS.HISFC.Models.Base.Bed();
            //床位号为病区加+床号
            bed.ID = adtA10.PV1.AssignedPatientLocation.PointOfCare.Value + adtA10.PV1.AssignedPatientLocation.Bed.Value;
            bed.InpatientNO = adtA10.PV1.VisitNumber.ID.Value; //住院流水号

            #region 记录操作人//接诊护士
            FS.FrameWork.Management.Connection.Operator.ID = adtA10.EVN.GetOperatorID(0).IDNumber.Value;
            if (string.IsNullOrEmpty(FS.FrameWork.Management.Connection.Operator.ID))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "接诊失败，原因：操作员编码为空";
                return -1;
            }
            FS.FrameWork.Management.Connection.Operator = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetEmployee(FS.FrameWork.Management.Connection.Operator.ID);
            if (FS.FrameWork.Management.Connection.Operator == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "接诊失败，原因：传入的操作员编码，系统中找不到" + adtA10.EVN.GetOperatorID(0).IDNumber.Value;
                return -1;
            }

            #endregion


            //责任护士
            #region 住院医生
            patientInfo.PVisit.AdmittingDoctor.ID = adtA10.PV1.GetAttendingDoctor(0).IDNumber.Value;
            //chenxin 2012-10-24 接诊不判断医生是否没有
            //if (string.IsNullOrEmpty(patientInfo.PVisit.AdmittingDoctor.ID))
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    errInfo = "接诊失败，原因：住院医生编码为空";
            //    return -1;
            //}
            if (string.IsNullOrEmpty(patientInfo.PVisit.AdmittingDoctor.ID) ==false)
            {
                patientInfo.PVisit.AdmittingDoctor = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetEmployee(patientInfo.PVisit.AdmittingDoctor.ID);
            }
            //if (patientInfo.PVisit.AdmittingDoctor == null)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    errInfo = "接诊失败，原因：传入的住院医生编码，系统中找不到" + adtA10.PV1.GetAttendingDoctor(0).IDNumber.Value;
            //    return -1;
            //}
            #endregion
            //主治医生
            patientInfo.PVisit.AttendingDoctor.ID = adtA10.PV1.GetAdmittingDoctor(0).IDNumber.Value;
            //主任医生

            //开启事务
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.RADTIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (RADTIntegrate.ArrivePatient(patientInfo, bed) == -1)//调用接诊业务
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "接诊失败，原因：" + this.RADTIntegrate.Err;
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

    }
}
