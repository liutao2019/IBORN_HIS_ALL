using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Components.Pharmacy.Maintenance
{
    public enum EnumSplitType
    {
        最小单位总量取整 = 0,
        包装单位总量取整 = 1,
        最小单位每次取整 = 2,
        包装单位每次取整 = 3,
        最小单位可拆分 = 4,
        End
    }
}
