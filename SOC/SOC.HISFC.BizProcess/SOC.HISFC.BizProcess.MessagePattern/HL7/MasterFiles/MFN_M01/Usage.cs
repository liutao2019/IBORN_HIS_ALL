using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01
{
    public class Usage
    {
        public int Receive(NHapi.Model.V24.Message.MFN_M01 HL7Usage, ref string errInfo)
        {
            FS.HISFC.Models.Base.Const constant = new FS.HISFC.Models.Base.Const();
            FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();  

            return constMgr.SetConstant("USAGE", constant);

        }
    }
}
