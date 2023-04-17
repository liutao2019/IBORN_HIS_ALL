using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class RAS_O17_Receiver : AbstractReceiver<NHapi.Model.V24.Message.RAS_O17, NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.RAS_O17 o, ref NHapi.Base.Model.IMessage ackMessage)
        {
            return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.RAS_O17.InpatientOrderApply().ProcessMessage(o, ref ackMessage,ref this.errInfo);
        }
    }
}
