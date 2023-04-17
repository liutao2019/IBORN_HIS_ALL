using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.CommonInterface.Exceptions
{
    [Serializable]
    public class InterfaceLoadException:Exception
    {
        public InterfaceLoadException(string exception, Exception cause)
            : base(exception, cause)
        {

        }

        public InterfaceLoadException(string exception)
            : base(exception)
        {

        }
    }
}
