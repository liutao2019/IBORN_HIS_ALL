using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy
{
    public interface IItemExtendControl
    {
        int Init(ref string errInfo);
        int Set(FS.HISFC.Models.Pharmacy.Item item, ref string errInfo);
        FS.HISFC.Models.Pharmacy.Item Get(ref string errInfo);
        int CheckValid();
        int Clear();
        int Save(FS.HISFC.Models.Pharmacy.Item item, ref string errInfo);
    }
}
