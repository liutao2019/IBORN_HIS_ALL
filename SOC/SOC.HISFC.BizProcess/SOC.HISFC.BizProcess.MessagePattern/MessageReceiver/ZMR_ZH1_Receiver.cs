using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class ZMR_ZH1_Receiver:AbstractReceiver<NHapi.Model.V24.Message.ZMR_ZH1,NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.ZMR_ZH1 o, ref NHapi.Base.Model.IMessage ackMessage)
        {
            return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MedicalRecordsInformationManagement.ZMR_ZH1.HealthRecord().ProcessMessage(o, ref ackMessage, ref errInfo);
        }
    }
}
