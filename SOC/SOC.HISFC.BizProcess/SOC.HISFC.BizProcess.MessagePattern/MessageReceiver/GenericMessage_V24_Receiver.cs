using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class GenericMessage_V24_Receiver:AbstractReceiver<NHapi.Base.Model.GenericMessage,NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Base.Model.GenericMessage o, ref NHapi.Base.Model.IMessage ackMessage)
        {
            FS.HL7Message.Model.MessageObject messageObject = new FS.HL7Message.Model.MessageObject();
            messageObject.MessageObjectStatus = FS.HL7Message.Model.MessageObject.Status.SUCCESS;
            messageObject.Errors = "--HIS只接收，暂未处理";
            ackMessage = FS.HL7Message.V24.Function.GenerateACK(o, messageObject);
            return 1;
        }
    }
}
