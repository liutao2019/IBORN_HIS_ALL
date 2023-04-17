using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageSender
{

    public class ADT_A04_Sender : AbstractSender<Object, NHapi.Model.V24.Message.ADT_A01, NHapi.Model.V24.Message.ACK, FS.FrameWork.Models.NeuObject>
    {
  
        protected override int ConvertObjectToSendMessage(Object t, ref NHapi.Model.V24.Message.ADT_A01 e, params object[] appendParams)
        {
            if (t is FS.HISFC.Models.Registration.Register)  //门诊登记
            {
                return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A04.OutPatientRegister().ProcessMessage(t as FS.HISFC.Models.Registration.Register, ref e, ref this.errInfo);
            }
            else if (t is FS.HISFC.HealthCheckup.Object.ChkRegister)  //体检登记
            {
                return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A04.HealthCheckupRegister().ProcessMessage(t as FS.HISFC.HealthCheckup.Object.ChkRegister, ref e, ref this.errInfo);
            }
            else
            {
                errInfo = string.Format("未知的类型T={0}", typeof(object));
                return -1;
            }
        }
    }

 
}
