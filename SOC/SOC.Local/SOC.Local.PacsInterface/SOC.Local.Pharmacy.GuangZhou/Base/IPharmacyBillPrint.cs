using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.Local.Pharmacy.Base
{
    interface IPharmacyBillPrint
    {
        int SetPrintData(System.Collections.ArrayList alPrintData, Base.PrintBill printBill);
    }
}
