using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class OMG_O19_Receiver : AbstractReceiver<NHapi.Model.V24.Message.OMG_O19, NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.OMG_O19 omg019, ref NHapi.Base.Model.IMessage ackMessage)
        {
            string patientType = omg019.PATIENT.PATIENT_VISIT.PV1.PatientClass.Value;
            string orcType = omg019.GetORDER(0).ORC.OrderControl.Value;
            //门诊检查申请
            if (patientType.Equals("O"))
            {
                //检查的取消终端确认
                if (orcType.Equals("OC"))
                {
                    return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OMG_O19.OutpatientExaminationTerminalCancelConfirm().ProcessMessage(omg019, ref ackMessage,ref this.errInfo);
                }
                //检查申请
                else
                {
                    return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OMG_O19.OutpatientExaminationApply().ProcessMessage(omg019, ref ackMessage,ref this.errInfo);
                }
            }
            //住院检查申请，取消检查申请
            else if (patientType.Equals("I"))
            {
                //检查的取消终端确认
                if (orcType.Equals("OC"))
                {
                    return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OMG_O19.InpatientExaminationTerminalCancelConfirm().ProcessMessage(omg019, ref ackMessage, ref this.errInfo);
                }
                //检查申请，从接收医嘱取
                else
                {
                    this.Err = "暂时不处理住院检查申请";
                    return 1;
                }
            }
            //体检检查取消终端确认
            else if (patientType.Equals("T") || patientType.Equals("J"))
            {
                if (orcType.Equals("OC"))
                {
                    return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OMG_O19.HealthCheckupExaminationTerminalCancelConfirm().ProcessMessage(omg019, ref ackMessage, ref this.errInfo);
                }
                else
                {
                    this.Err = "暂时不处理住院检查申请";
                    return 1;
                }
            }

            return 1;
        }
    }
}
