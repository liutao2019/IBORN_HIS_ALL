using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A32
{
    /// <summary>
    /// 取消接诊
    /// </summary>
    public class InPatientCancelArrive
    {
        FS.HISFC.BizProcess.Integrate.RADT RADTIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 取消接诊信息处理
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int ProcessMessage(NHapi.Model.V24.Message.ADT_A32 adtA32, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            if (string.IsNullOrEmpty(adtA32.PV1.VisitNumber.ID.Value))
            {
                errInfo = "取消接诊失败，原因：患者流水号为空";
                return -1;
            }

            FS.FrameWork.Management.Connection.Operator.ID = adtA32.EVN.GetOperatorID(0).IDNumber.Value;
            if (string.IsNullOrEmpty(FS.FrameWork.Management.Connection.Operator.ID))
            {
                FS.FrameWork.Management.Connection.Operator.ID = "T00001";
                errInfo = "取消接诊失败，原因：操作员编码为空";
                //return -1;
            }
            if (CommonController.CreateInstance().GetEmployee(FS.FrameWork.Management.Connection.Operator.ID) != null)
            {
                FS.FrameWork.Management.Connection.Operator = CommonController.CreateInstance().GetEmployee(FS.FrameWork.Management.Connection.Operator.ID);

            }
            else
            {
                errInfo = "取消接诊失败，原因：传入的操作员编码，系统中找不到" + adtA32.EVN.GetOperatorID(0).IDNumber.Value;
                //return -1;
            }



            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.radtManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.RADTIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.HISFC.Models.RADT.PatientInfo patientInfo =  RADTIntegrate.QueryPatientInfoByInpatientNO(adtA32.PV1.VisitNumber.ID.Value);
            if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "取消接诊获取" + adtA32.PV1.VisitNumber.ID.Value + "患者信息失败，原因：" + this.RADTIntegrate.Err;
                return -1;
            }

            FS.HISFC.Models.Base.Bed bed = new FS.HISFC.Models.Base.Bed();
            bed.InpatientNO = patientInfo.ID;
            if (radtManager.CancelRecievePatient(patientInfo, bed, FS.HISFC.Models.Base.EnumShiftType.K, "") <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "取消接诊失败，原因：" + this.radtManager.Err;
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }
    }
}
