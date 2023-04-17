using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    /// <summary>
    /// 接收体检检查报告
    /// </summary>
    public class ORU_R01_Receiver : AbstractReceiver<NHapi.Model.V24.Message.ORU_R01, NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.ORU_R01 orur01, ref NHapi.Base.Model.IMessage ackMessage)
        {
            string patienttype = orur01.GetPATIENT_RESULT().PATIENT.VISIT.PV1.PatientClass.Value;
            if (patienttype.Equals("T") || patienttype.Equals("J"))  //接收体检检查报告
            {
                return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORU_R01.HealthCheckupExaminationReport().ProcessMessage(orur01, ref ackMessage, ref errInfo);
            }
            else
            {

            }
            return 1;
        }
    }
}
