using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class ORR_O02_Receiver : AbstractReceiver<NHapi.Model.V24.Message.ORR_O02, NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.ORR_O02 orrO02, ref NHapi.Base.Model.IMessage ackMessage)
        {
            //判断是门诊还是住院
            if (orrO02.RESPONSE.ORDERRepetitionsUsed > 0)
            {
                string patientType = orrO02.RESPONSE.GetORDER(0).ORC.PlacerOrderNumber.UniversalIDType.Value;
                //门诊
                if (patientType.Equals("O"))
                {

                }
                //住院
                else if (patientType.Equals("I"))
                {
                    return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORR_O02.InpatientOperationTerminalConfirm().ProcessMessage(orrO02, ref ackMessage, ref this.errInfo);
                }
                //体检
                else if (patientType.Equals("P"))
                {
                }
                else
                {
                    this.Err = "未知的患者类型";
                    return -1;
                }

            }

            return 1;

        }
    }
}
