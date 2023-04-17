using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Registration.ZhuHai.Zdwy.IPrintLable
{
    public class PrintLableDef : FS.HISFC.BizProcess.Interface.Account.IPrintLable
    {

        ucPrintLable ucprintLable = null;

        public void PrintLable(FS.HISFC.Models.Account.AccountCard accountCard)
        {
            if (ucprintLable == null)
            {
                ucprintLable = new ucPrintLable();
            }

            ucprintLable.SetValue(accountCard);

            ucprintLable.Print();
        }
    }
}