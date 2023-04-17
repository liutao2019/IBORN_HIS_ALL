using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore
{
    /// <summary>
    /// 住院药房单据类型
    /// </summary>
    public enum InpatientBillType
    {
        汇总,
        明细,
        出院带药处方,
        草药,
        药袋,
        标签
    }
}