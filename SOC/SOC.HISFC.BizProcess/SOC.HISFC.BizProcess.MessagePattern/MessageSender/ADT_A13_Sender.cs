using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageSender
{
    public class ADT_A13_Sender : AbstractSender<FS.HISFC.Models.RADT.PatientInfo, NHapi.Model.V24.Message.ADT_A01, NHapi.Model.V24.Message.ACK, FS.FrameWork.Models.NeuObject>
    {
        protected override int ConvertObjectToSendMessage(FS.HISFC.Models.RADT.PatientInfo t, ref NHapi.Model.V24.Message.ADT_A01 e, params object[] appendParams)
        {
            return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A13.PatientCancelBalance().ProcessMessage(t, ref e, ref this.errInfo);
        }
    }
}
