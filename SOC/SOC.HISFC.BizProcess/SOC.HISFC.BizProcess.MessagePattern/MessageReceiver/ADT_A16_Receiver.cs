using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class ADT_A16_Receiver : AbstractReceiver<NHapi.Model.V24.Message.ADT_A16, NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.ADT_A16 o, ref NHapi.Base.Model.IMessage ackMessage)
        {
            return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A16.PatientOut().ProcessMessage(o, ref ackMessage, ref this.errInfo);
        }
    }
}
