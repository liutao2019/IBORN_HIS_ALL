using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageSender
{
    public class ORL_O22_Sender : AbstractSender<object , NHapi.Model.V24.Message.ORL_O22, NHapi.Model.V24.Message.ACK, FS.FrameWork.Models.NeuObject>
    {
        protected override int ConvertObjectToSendMessage(object t, ref NHapi.Model.V24.Message.ORL_O22 e, params object[] appendParams)
        {
            if (t is FS.HISFC.Models.Registration.Register) //门诊系统
            {
                return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORL_O22.OutpatientInspectionFeeConfirm().ProcessMessage(t as FS.HISFC.Models.Registration.Register, appendParams[0] as ArrayList, (Boolean)appendParams[1], ref e, ref this.errInfo);

            }
            else if (t is FS.HISFC.HealthCheckup.Object.ChkRegister)  //体检系统
            {
                return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORL_O22.HealthCheckupInspectionFeeConfirm().ProcessMessage(t as FS.HISFC.HealthCheckup.Object.ChkRegister, appendParams[0] as ArrayList, (Boolean)appendParams[1], ref e, ref this.errInfo);
            }
            else 
            {
                errInfo = string.Format("未知的类型T={0}", typeof(object));
                return -1;
            }
        }

   
    }
}
