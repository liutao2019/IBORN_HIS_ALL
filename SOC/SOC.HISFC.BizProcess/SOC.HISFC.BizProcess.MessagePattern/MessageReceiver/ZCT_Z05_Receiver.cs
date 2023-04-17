using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class ZCT_Z05_Receiver : AbstractReceiver<NHapi.Model.V24.Message.ZCT_Z05, NHapi.Base.Model.IMessage>
    {

        public override int ProcessMessage(NHapi.Model.V24.Message.ZCT_Z05 o, ref NHapi.Base.Model.IMessage ackMessage)
        {
            FS.HL7Message.Model.MessageObject messageObject = new FS.HL7Message.Model.MessageObject();
            messageObject.MessageObjectStatus = FS.HL7Message.Model.MessageObject.Status.FILTERED;
            messageObject.Errors = "拒绝响应";
            ackMessage = FS.HL7Message.V24.Function.GenerateACK(o, messageObject);
            // ackMessage.MSA.AcknowledgementCode.Value = "AR";
            return 1;
        }
    }
}
