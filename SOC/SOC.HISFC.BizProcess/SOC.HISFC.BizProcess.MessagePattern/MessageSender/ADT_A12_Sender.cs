using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageSender
{
    class ADT_A12_Sender : AbstractSender<FS.HISFC.Models.Nurse.Assign, NHapi.Model.V24.Message.ADT_A09, NHapi.Model.V24.Message.ACK, FS.FrameWork.Models.NeuObject>
    {
        protected override int ConvertObjectToSendMessage(FS.HISFC.Models.Nurse.Assign t, ref NHapi.Model.V24.Message.ADT_A09 e, params object[] appendParams)
        {
            return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A12.OutPatientCancelAssign().ProcessMessage(t, ref e, ref this.errInfo);
        }
    }
}
