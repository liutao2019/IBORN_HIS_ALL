using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class ACK_V24_Receiver : AbstractReceiver<NHapi.Model.V24.Message.ACK, NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.ACK o, ref NHapi.Base.Model.IMessage ackMessage)
        {
            FS.HL7Message.Model.MessageObjectController messageController = new FS.HL7Message.Model.MessageObjectController();
            FS.HL7Message.Model.MessageObject obj = messageController.GetMessageObject("Source", o.MSH.MessageControlID.Value);
            if (obj == null)
            {
                this.errInfo = "未找到消息ID为" + o.MSH.MessageControlID.Value + "原始消息!";
                return -1;
            }
            NHapi.Base.Parser.PipeParser parser = new NHapi.Base.Parser.PipeParser();
            string messageStr = parser.Encode(o);
            obj.TransformedData = messageStr;
            messageController.UpdateMessageTransformedData(obj);

            FS.HL7Message.Server.ResponseAck rack = new FS.HL7Message.Server.ResponseAck(messageStr);
            if (rack.GetTypeOfAck())
            {
                messageController.SetSuccess(obj, "消息发送成功");
                return 1;
            }
            else
            {
                this.errInfo = "消息确认结果：" + rack.ErrorDescription;
                return -1;
            }
        }
    }
}
