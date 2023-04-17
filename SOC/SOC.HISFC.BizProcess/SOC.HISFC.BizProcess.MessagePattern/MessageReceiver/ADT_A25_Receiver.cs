using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class ADT_A25_Receiver : AbstractReceiver<NHapi.Model.V24.Message.ADT_A25, NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.ADT_A25 o, ref NHapi.Base.Model.IMessage ackMessage)
        {
            return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A25.PatientCallBack().ProcessMessage(o, ref ackMessage, ref this.errInfo);
        }
    }
}
