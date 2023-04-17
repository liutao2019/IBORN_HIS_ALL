using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class ADT_A61_Receiver : AbstractReceiver<NHapi.Model.V24.Message.ADT_A61, NHapi.Base.Model.IMessage>
    {
         public override int ProcessMessage(NHapi.Model.V24.Message.ADT_A61 o, ref NHapi.Base.Model.IMessage ackMessage)
         {
             return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A61.DoctorLoginConsole().ProcessMessage(o, ref ackMessage, ref this.errInfo);
         }
    }
}
