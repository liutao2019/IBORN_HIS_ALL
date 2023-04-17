using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Registration.ShenZhen.BinHai.IRegPrint
{
    public class RegAndBarCodePrint : FS.HISFC.BizProcess.Interface.Registration.IRegPrint
    {
        ucRegInvoicePrint ucRegPrint = null;

        #region IRegPrint 成员

        public int Clear()
        {
            return 1;
        }

        public int Print()
        {
            if (ucRegPrint != null)
            {
                return ucRegPrint.Print();
            }

            return 1;
        }

        public int PrintView()
        {
            return 1;
        }

        public int SetPrintValue(FS.HISFC.Models.Registration.Register register)
        {
            if (register.PrintInvoiceCnt == 0 || register.PrintInvoiceCnt == 1)
            {
                ucRegPrint = new ucRegInvoicePrint();
                ucRegPrint.SetPrintValue(register);
            }

            return 1;
        }

        public void SetTrans(System.Data.IDbTransaction trans)
        {
        }

        public System.Data.IDbTransaction Trans
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        #endregion
    }
}
