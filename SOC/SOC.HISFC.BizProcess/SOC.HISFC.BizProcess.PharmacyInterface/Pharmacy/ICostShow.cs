using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy
{
    /// <summary>
    /// 金额显示接口
    /// </summary>
    public interface ICostShow
    {
        string GenerateCostShow(string[] args);
    }
}
