using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Pharmacy
{
    public class BillPrintFun : FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint
    {
        #region IBillPrint ��Ա

        public int Prieview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Print()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SetData(System.Collections.ArrayList alPrintData, FS.HISFC.BizProcess.Interface.Pharmacy.BillType billType)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SetData(System.Collections.ArrayList alPrintData, string privType)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SetData(string billNO)
        {
            return 1;
        }

        #endregion
    }
}
