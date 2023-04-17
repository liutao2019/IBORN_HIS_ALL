using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class OML_O21_Receiver : AbstractReceiver<NHapi.Model.V24.Message.OML_O21, NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.OML_O21 omlO21, ref NHapi.Base.Model.IMessage ackMessage)
        {
            string patientType = omlO21.PATIENT.PATIENT_VISIT.PV1.PatientClass.Value;
            //门诊检验申请
            if (patientType.Equals("O"))
            {
                return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OML_O21.OutpatientInspectionApply().ProcessMessage(omlO21, ref ackMessage, ref this.errInfo);
            }
            //住院检验申请
            else if (patientType.Equals("I"))
            {
                this.Err = "暂时不处理住院检验申请";
                return 1;
            }

            return 1;
        }
    }
}
