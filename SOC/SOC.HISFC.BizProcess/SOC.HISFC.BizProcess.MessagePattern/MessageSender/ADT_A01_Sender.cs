using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageSender
{
    public class ADT_A01_Sender:AbstractSender<object ,NHapi.Model.V24.Message.ADT_A01,NHapi.Model.V24.Message.ACK,FS.FrameWork.Models.NeuObject>
    {
        protected override int ConvertObjectToSendMessage(object t, ref NHapi.Model.V24.Message.ADT_A01 e, params object[] appendParams)
        {
            if (t is FS.HISFC.Models.RADT.PatientInfo)
            {
                return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A01.InPatientRegister().ProcessMessage(t as FS.HISFC.Models.RADT.PatientInfo, ref e, ref this.errInfo);
            }
            else 
            {
                errInfo = string.Format("未知的类型T={0}", typeof(object));
                return -1;
            }
        }
    }
}
