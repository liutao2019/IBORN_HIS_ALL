using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Terminal.Confirm
{
    public interface TerminalInterface
    {
        int Reset();

        int ControlValue(Object obj);
        int Print();
    }
}
