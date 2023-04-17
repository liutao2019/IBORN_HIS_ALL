using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageSender
{
    public class ADT_A02_Sender : AbstractSender<FS.SOC.HISFC.Assign.Models.Assign, NHapi.Model.V24.Message.ADT_A02, NHapi.Model.V24.Message.ACK, FS.FrameWork.Models.NeuObject>
    {
        protected override int ConvertObjectToSendMessage(FS.SOC.HISFC.Assign.Models.Assign t, ref NHapi.Model.V24.Message.ADT_A02 e, params object[] appendParams)
        {
            return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A02.OutPatientAssign().ProcessMessage(t, ref e, ref this.errInfo);
        }
    }
}
