using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HISFC.Models.Base;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class SRM_S01_Receiver : AbstractReceiver<NHapi.Model.V24.Message.SRM_S01, NHapi.Base.Model.IMessage>
    {
        private int processMessage(NHapi.Model.V24.Message.SRM_S01 o, ref NHapi.Model.V24.Message.SRR_S01 ackMessage)
        {
            if (o.ARQ.AppointmentReason.Identifier.Value == "APPT_REG") //预约挂号
            {
                return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Scheduling.SRM_S01.BookingOrder().processMessage(o, ref ackMessage, ref this.errInfo);
            }
            else  //自助挂号
            {
                return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Scheduling.SRM_S01.PreRegister().ProcessMessage(o, ref ackMessage, ref this.errInfo);
            }
        }

        #region IAcceptMessage<SRM_S01,IMessage> 成员

        public override int ProcessMessage(NHapi.Model.V24.Message.SRM_S01 o, ref NHapi.Base.Model.IMessage ackMessage)
        {
     
            NHapi.Model.V24.Message.SRR_S01 SRRS01 = ackMessage as NHapi.Model.V24.Message.SRR_S01;
            int i = this.processMessage(o, ref SRRS01);

            FS.HL7Message.V24.Function.GenerateMSH(SRRS01.MSH, o.MSH);

            if (i < 0)
            {
                FS.HL7Message.V24.Function.GenerateErrorMSA(o.MSH, SRRS01.MSA,this.errInfo);
                SRRS01.MSA.ExpectedSequenceNumber.Value = "200";
            }
            else
            {
                FS.HL7Message.V24.Function.GenerateSuccessMSA(o.MSH, SRRS01.MSA);
                SRRS01.MSA.ExpectedSequenceNumber.Value = "100";
            }

            ackMessage = SRRS01;

             return i;
          
        }

        #endregion

    
    }
}
