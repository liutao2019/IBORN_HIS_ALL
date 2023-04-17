using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class ORG_O20_Receiver : AbstractReceiver<NHapi.Model.V24.Message.ORG_O20, NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.ORG_O20 orgO20, ref NHapi.Base.Model.IMessage ackMessage)
        {
            //判断是门诊还是住院
            if (orgO20.RESPONSE.ORDERRepetitionsUsed > 0)
            {
                string patientType = orgO20.RESPONSE.GetORDER(0).ORC.PlacerOrderNumber.UniversalIDType.Value;
                //门诊
                if (patientType.Equals("O"))
                {
                    return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORG_O20.OutpatientExaminationTerminalConfirm().ProcessMessage(orgO20, ref ackMessage,ref this.errInfo);
                }
                //住院
                else if (patientType.Equals("I"))
                {
                    return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORG_O20.InpatientExaminationTerminalConfirm().ProcessMessage(orgO20, ref ackMessage, ref this.errInfo);
                }
                //体检
                else if (patientType.Equals("T")|| patientType.Equals("J"))
                {
                    return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORG_O20.HealthCheckupExaminationTerminalConfirm().ProcessMessage(orgO20, ref ackMessage, ref this.errInfo);
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
