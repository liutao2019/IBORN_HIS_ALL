using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Registration
{
    /// <summary>
    /// ДђгЁЬѕТы{D2F77BDA-F5E5-48fe-AB73-B7FE6D92E6E2}
    /// </summary>
    public interface IPrintBar
    {
        int printBar(FS.HISFC.Models.RADT.Patient p ,ref string errText);
    }
}
