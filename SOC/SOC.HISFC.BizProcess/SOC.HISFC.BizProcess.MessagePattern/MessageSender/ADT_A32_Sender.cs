using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageSender
{
    public class ADT_A32_Sender : AbstractSender<FS.HISFC.Models.Nurse.Assign, NHapi.Model.V24.Message.ADT_A32, NHapi.Model.V24.Message.ACK, FS.FrameWork.Models.NeuObject>
    {
        protected override int ConvertObjectToSendMessage(FS.HISFC.Models.Nurse.Assign t, ref NHapi.Model.V24.Message.ADT_A32 e, params object[] appendParams)
        {
            return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A32.OutPatientCancelInRoom().ProcessMessage(t, ref e, ref this.errInfo);
        }
    }
}
