﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Base
{
    interface IPharmacyBillPrint
    {
        int SetPrintData(System.Collections.ArrayList alPrintData, Base.PrintBill printBill);
    }
}
