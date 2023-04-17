using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageSender
{
    public class ORP_O10_Sender : AbstractSender<FS.HISFC.Models.Registration.Register, NHapi.Model.V24.Message.ORP_O10, NHapi.Model.V24.Message.ACK, FS.FrameWork.Models.NeuObject>
    {
        protected override int ConvertObjectToSendMessage(FS.HISFC.Models.Registration.Register t, ref NHapi.Model.V24.Message.ORP_O10 e, params object[] appendParams)
        {
            return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORP_O10.OutpatientRecipeFeeConfirm().ProcessMessage(t, appendParams[0] as ArrayList, (Boolean)appendParams[1], ref e, ref this.errInfo);
        }
    }
}
