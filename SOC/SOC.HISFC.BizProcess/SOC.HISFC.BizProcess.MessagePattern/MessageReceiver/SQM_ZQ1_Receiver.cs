using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HISFC.Models.Base;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class SQM_ZQ1_Receiver : AbstractReceiver<NHapi.Model.V24.Message.SQM_ZQ1, NHapi.Model.V24.Message.SQR_ZQ1[]>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.SQM_ZQ1 o, ref NHapi.Model.V24.Message.SQR_ZQ1[] ackMessage)
        {
            if (o == null)
            {
                this.Err = "消息内容为空！";
                return -1;
            }
            return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.SQM_ZQ1.QueryBookingConfirm().ProcessMessage(o,ref ackMessage, ref errInfo);
        }

 
    }
}
