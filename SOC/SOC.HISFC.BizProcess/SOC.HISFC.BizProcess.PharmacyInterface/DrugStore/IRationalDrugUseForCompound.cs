using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore
{
    public interface IRationalDrugUseForCompound
    {
        /// <summary>
        /// 检验药品申请信息
        /// </summary>
        /// <param name="alOrder">药品申请信息</param>
        /// <returns></returns>
        int CheckDrugUse(ArrayList alApplyOut);
    }
}
