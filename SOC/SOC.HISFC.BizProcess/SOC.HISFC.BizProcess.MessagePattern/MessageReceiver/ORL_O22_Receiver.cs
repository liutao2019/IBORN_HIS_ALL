using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class ORL_O22_Receiver: AbstractReceiver<NHapi.Model.V24.Message.ORL_O22, NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.ORL_O22 orlO22, ref NHapi.Base.Model.IMessage ackMessage)
        {
            //判断是门诊还是住院
            if (orlO22.RESPONSE.PATIENT.GENERAL_ORDERRepetitionsUsed > 0 && orlO22.RESPONSE.PATIENT.GetGENERAL_ORDER(0).ORDERRepetitionsUsed > 0)
            {
                string patientType = orlO22.RESPONSE.PATIENT.GetGENERAL_ORDER(0).GetORDER(0).ORC.PlacerOrderNumber.UniversalIDType.Value;
                string orcType = orlO22.RESPONSE.PATIENT.GetGENERAL_ORDER().GetORDER().ORC.OrderControl.Value;
                if (string.IsNullOrEmpty(patientType))
                {
                    this.Err = "患者类型为空，ORC-1";
                    return -1;
                }
                //门诊
                else if (patientType.Equals("O"))
                {
                    return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORL_O22.OutpatientInspectionTerminalConfirm().ProcessMessage(orlO22, ref ackMessage, ref this.errInfo);
                }
                //住院
                else if (patientType.Equals("I"))
                {
                    return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORL_O22.InpatientInspectionTerminalConfirm().ProcessMessage(orlO22, ref ackMessage, ref this.errInfo);
                }
                //体检
                else if (patientType.Equals("T")|| patientType.Equals("J"))
                {
                  return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORL_O22.HealthCheckupInspectionTerminalConfirm().ProcessMessage(orlO22, ref ackMessage, ref this.errInfo);
                }
                else
                {
                    this.Err = "未知的患者类型";
                    return -1;
                }
            }

            return 1;
        }
    }
}
