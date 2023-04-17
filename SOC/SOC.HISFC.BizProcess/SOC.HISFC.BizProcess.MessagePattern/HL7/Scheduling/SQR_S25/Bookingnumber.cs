using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Scheduling.SQR_S25
{
    public class Bookingnumber
    {
    

        internal int processMessaege(NHapi.Model.V24.Message.SQR_S25 sQR_S25, ref object resultSingleInfo, ref string errInfo)
        {
            FS.HISFC.Models.Registration.Schema schema = new FS.HISFC.Models.Registration.Schema();
            if (sQR_S25 == null)
            {
                return 1;
            }
            schema.Templet.ID = sQR_S25.GetSCHEDULE().GetRESOURCES().GetGENERAL_RESOURCE().AIG.ResourceID.Identifier.Value;
            schema.TeledQTY = FS.FrameWork.Function.NConvert.ToDecimal(sQR_S25.GetSCHEDULE().GetRESOURCES().GetGENERAL_RESOURCE().AIG.ResourceQuantity.Value);
            resultSingleInfo = schema;
            return 1;
        }
    }
}
