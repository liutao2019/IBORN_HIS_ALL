using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.CallQueue.Interface
{
    /// <summary>
    /// 叫号
    /// </summary>
    public  interface ICallSpeak
    {
        /// <summary>
        /// 叫号
        /// </summary>
        /// <param name="nurseAssign"></param>
        void Speech(FS.SOC.HISFC.CallQueue.Models.NurseAssign nurseAssign);
    }
}
