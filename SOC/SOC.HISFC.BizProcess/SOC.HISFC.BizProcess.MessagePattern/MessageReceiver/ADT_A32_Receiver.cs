using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class ADT_A32_Receiver : AbstractReceiver<NHapi.Model.V24.Message.ADT_A32, NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.ADT_A32 o, ref NHapi.Base.Model.IMessage ackMessage)
        {
            switch (o.PV1.PatientClass.Value)
            {
                case "O"://门诊
                    return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A32.OutpatientCancelSeeOut().ProcessMessage(o, ref ackMessage, ref this.errInfo);
                case "I"://住院
                    return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A32.InPatientCancelArrive().ProcessMessage(o, ref ackMessage, ref this.errInfo);
                case "P"://体检
                default:
                    return 1;
            }
        }
    }
}
