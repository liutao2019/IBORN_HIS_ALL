using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class RRG_O16_Receiver:AbstractReceiver<NHapi.Model.V24.Message.RRG_O16, NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.RRG_O16 o, ref NHapi.Base.Model.IMessage ackMessage)
        {
            return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.RRG_O16.OutPatientDrugReceiveRowa().ProcessMessage(o, ref ackMessage,ref this.errInfo);
        }
    }
}
