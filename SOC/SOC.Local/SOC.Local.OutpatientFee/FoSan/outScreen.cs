using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.OutpatientFee.FoSan
{
    class outScreen : FS.HISFC.BizProcess.Interface.FeeInterface.IOutScreen
    {
        FS.SOC.Local.OutpatientFee.FoSan.Function fun = new Function();


        #region IOutScreen 成员

        public int ClearInfo()
        {
            try
            {
                fun.SetClear();
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        public int ShowInfo(FS.HISFC.Models.Registration.Register register)
        {
            string name = register.Name.ToString();
            try
            {
                fun.SetClear();
                fun.SetFont(12);
                fun.SetPrintText(40, 1, "姓名：      " + name);
                if (name == null || name == "")
                {
                }
                else
                {
                    fun.SetSoundWait(name);
                }
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        #endregion
    }
}
