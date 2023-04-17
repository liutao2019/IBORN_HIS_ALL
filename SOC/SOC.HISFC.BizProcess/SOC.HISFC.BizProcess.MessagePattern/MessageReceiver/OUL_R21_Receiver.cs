using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    /// <summary>
    /// 接收体检检验报告
    /// </summary>
    public class OUL_R21_Receiver : AbstractReceiver<NHapi.Model.V24.Message.OUL_R21, NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.OUL_R21 oulr21, ref NHapi.Base.Model.IMessage ackMessage)
        {
            string patienttype = oulr21.VISIT.PV1.PatientClass.Value;
            if (patienttype == "T" || patienttype == "J") //体检
            {
                return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OUL_R21.HealthCheckupInspectionReport().ProcessMessage(oulr21, ref ackMessage, ref errInfo);
            }
            else if( patienttype == "O" )//门诊
            { 
            
            }
            else if (patienttype.Equals("I")) //住院
            {

            }
            else 
            {
                return -1;
            }
            return 1;
        }
    }
}
