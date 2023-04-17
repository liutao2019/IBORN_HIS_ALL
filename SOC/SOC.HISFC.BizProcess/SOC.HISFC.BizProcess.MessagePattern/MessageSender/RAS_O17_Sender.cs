using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageSender
{
    public class RAS_O17_Sender : AbstractSender<FS.HISFC.Models.RADT.PatientInfo, NHapi.Model.V24.Message.RAS_O17, NHapi.Model.V24.Message.ACK, FS.FrameWork.Models.NeuObject>
    {
        protected override int ConvertObjectToSendMessage(FS.HISFC.Models.RADT.PatientInfo patientInfo, ref NHapi.Model.V24.Message.RAS_O17 e, params object[] appendParams)
        {
            return new HL7.OrderEntry.RAS_O17.InpatientFeeItem().ProcessFeeOrder(patientInfo, (appendParams[0] as ArrayList),(bool)appendParams[1], ref e, ref this.errInfo);
        }
    }
}
