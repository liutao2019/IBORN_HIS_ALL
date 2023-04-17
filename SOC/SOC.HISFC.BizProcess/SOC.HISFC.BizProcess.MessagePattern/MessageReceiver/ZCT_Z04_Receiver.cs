using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
   public class ZCT_Z04_Receiver :AbstractReceiver<NHapi.Model.V24.Message.ZCT_Z04, NHapi.Base.Model.IMessage>
    {

        public override int ProcessMessage(NHapi.Model.V24.Message.ZCT_Z04 o, ref NHapi.Base.Model.IMessage ackMessage)
        {
            FS.HL7Message.Model.MessageObject messageObject = new FS.HL7Message.Model.MessageObject();
            messageObject.MessageObjectStatus = FS.HL7Message.Model.MessageObject.Status.ERROR;
            messageObject.Errors = "错误响应";
            ackMessage = FS.HL7Message.V24.Function.GenerateACK(o, messageObject);
            // ackMessage.MSA.AcknowledgementCode.Value = "AR";
            return 1;
        }
    }
}
