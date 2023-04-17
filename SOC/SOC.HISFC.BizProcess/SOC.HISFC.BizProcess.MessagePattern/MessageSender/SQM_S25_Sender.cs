using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageSender
{
   public class SQM_S25_Sender:AbstractSender<FS.HISFC.Models.Registration.Schema ,NHapi.Model.V24.Message.SQM_S25,NHapi.Model.V24.Message.SQR_S25,object>
    {

       protected override int ConvertObjectToSendMessage(FS.HISFC.Models.Registration.Schema t, ref NHapi.Model.V24.Message.SQM_S25 e, params object[] appendParams)
       {
           return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Scheduling.SQM_S25.QueryBookingNumber().ProcessMessage(t, ref e, ref errInfo);
       }


       protected override int ConvertResultMessageToObject(NHapi.Model.V24.Message.SQR_S25 m, ref object resultSingleInfo, params object[] appendParams)
       {
           return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Scheduling.SQR_S25.Bookingnumber().processMessaege(m as NHapi.Model.V24.Message.SQR_S25, ref  resultSingleInfo, ref errInfo);
       }



       protected override SendMessageType SendMessageType
       {
           get
           {
               return SendMessageType.DoSending;
           }
       }


    }
}
