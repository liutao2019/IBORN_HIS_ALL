using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace FS.HISFC.Components.EPR.Interface
{

    public interface IContinuePrint
    {
        bool IsCanContinuePrint(Control panel);
        void Print(Control panel);
    }
}
