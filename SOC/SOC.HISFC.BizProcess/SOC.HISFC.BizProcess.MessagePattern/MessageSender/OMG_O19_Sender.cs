using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageSender
{
    public class OMG_O19_Sender : AbstractSender<FS.HISFC.HealthCheckup.Object.ChkRegister, NHapi.Model.V24.Message.OMG_O19[], NHapi.Model.V24.Message.ACK, FS.FrameWork.Models.NeuObject>
    {

        protected override int ConvertObjectToSendMessage(FS.HISFC.HealthCheckup.Object.ChkRegister t, ref NHapi.Model.V24.Message.OMG_O19[] e, params object[] appendParams)
        {
            return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OMG_O19.HealthCheckupExaminationApply().ProcessMessage(t, appendParams[0] as ArrayList, (Boolean)appendParams[1], ref e, ref this.errInfo);

        }
    }
}
