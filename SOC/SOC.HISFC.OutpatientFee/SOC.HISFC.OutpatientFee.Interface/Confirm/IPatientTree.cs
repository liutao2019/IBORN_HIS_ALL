using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.OutpatientFee.Interface.Confirm
{
    public interface IPatientTree
    {
        int Init();

        int Init(DateTime beginTime, DateTime endTime);

    }
}
