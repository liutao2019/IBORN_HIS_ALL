using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.RRG_O16
{
    class OutPatientDrugReceiveRowa
    {

        public int ProcessMessage(NHapi.Model.V24.Message.RRG_O16 rrgO16, ref NHapi.Base.Model.IMessage ackMessage , ref string errInfo)
        {
            if (rrgO16 == null)
            {
                return -1;
            }

            FS.HISFC.Models.Pharmacy.ApplyOut ApplyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();
           //ApplyOut.OrderNo = rrgO16.ERR
            return 1;
        }
    }
}
