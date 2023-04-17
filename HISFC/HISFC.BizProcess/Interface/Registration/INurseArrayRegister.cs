using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Registration
{
    public delegate void GetRegisterHander(ref FS.HISFC.Models.Registration.Register reg);
    public interface INurseArrayRegister
    {
        FS.HISFC.Models.RADT.Patient Patient
        {
            get;
            set;
        }
        event GetRegisterHander OnGetRegister;
    }
}
