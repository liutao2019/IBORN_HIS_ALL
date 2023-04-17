using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HISFC.Models.Base;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class SRM_S04_Receiver : AbstractReceiver<NHapi.Model.V24.Message.SRM_S04, NHapi.Base.Model.IMessage>
    {
        private int processMessage(NHapi.Model.V24.Message.SRM_S04 o, ref NHapi.Model.V24.Message.SRR_S04 ackMessage)
        {
            return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Scheduling.SRM_S04.CancelPreRegister().ProcessMessage(o, ref ackMessage,ref this.errInfo);
        }

        #region IAcceptMessage<SRM_S04,IMessage> 成员

        public override int ProcessMessage(NHapi.Model.V24.Message.SRM_S04 o, ref NHapi.Base.Model.IMessage ackMessage)
        {
            NHapi.Model.V24.Message.SRR_S04 SRR_S04 = ackMessage as NHapi.Model.V24.Message.SRR_S04;

            return this.processMessage(o, ref SRR_S04);
        }

        #endregion
    }
}
