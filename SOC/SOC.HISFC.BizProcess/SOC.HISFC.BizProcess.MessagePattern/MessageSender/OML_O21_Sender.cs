using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageSender
{
    public class OML_O21_Sender : AbstractSender<FS.HISFC.HealthCheckup.Object.ChkRegister, NHapi.Model.V24.Message.OML_O21[], NHapi.Model.V24.Message.ACK, FS.FrameWork.Models.NeuObject>
    {
        protected override int ConvertObjectToSendMessage(FS.HISFC.HealthCheckup.Object.ChkRegister t, ref NHapi.Model.V24.Message.OML_O21[] e, params object[] appendParams)
        {
            return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OML_O21.HealthCheckupInspectionApply().ProcessMessage(t, appendParams[0] as ArrayList, (Boolean)appendParams[1] , ref e, ref errInfo);
        }
    }
}
