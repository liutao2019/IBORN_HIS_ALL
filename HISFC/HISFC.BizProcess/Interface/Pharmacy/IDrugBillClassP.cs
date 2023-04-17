using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Pharmacy
{
    public interface IDrugBillClassP
    {
        FS.HISFC.Models.Pharmacy.DrugMessage GetDrugMessage(FS.HISFC.Models.Pharmacy.ApplyOut applyOut);
    }
}
