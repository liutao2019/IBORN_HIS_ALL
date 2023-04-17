﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class ADT_A01_Receiver : AbstractReceiver<NHapi.Model.V24.Message.ADT_A01, NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.ADT_A01 o, ref NHapi.Base.Model.IMessage ackMessage)
        {
            return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A01.BabyInPatientRegister().ProcessMessage(o,ref ackMessage,ref this.errInfo);
            //switch (o.PV1.PatientClass.Value)
            //{
            //    case "O"://门诊
            //    case "I"://住院
            //    case "P"://体检
            //    default:
            //        return 1;
            //}

        }
    }
}
