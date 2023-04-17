using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class OMP_O09_Receiver : AbstractReceiver<NHapi.Model.V24.Message.OMP_O09, NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.OMP_O09 ompO09, ref NHapi.Base.Model.IMessage ackMessage)
        {
            string patientType = ompO09.PATIENT.PATIENT_VISIT.PV1.PatientClass.Value;

            if (patientType.Equals("O"))
            {
                return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OMP_O09.OutPatientRecipeApply().ProcessMessage(ompO09, ref ackMessage, ref this.errInfo);
            }
            else if (patientType.Equals("I"))
            {
                this.Err = "暂时不处理住院医嘱";
                return 1;
            }
            return 1;
        }
    }
}
