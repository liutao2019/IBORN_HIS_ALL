using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore
{
    /// <summary>
    /// [功能描述: 配置中心单据打印接口]<br></br>
    /// [创 建 者: cao-lin]<br></br>
    /// [创建时间: 2013-02]<br></br>
    /// 功能说明：
    /// 1、实现本地化的打印
    /// </summary>
    public interface ICompoundPrint
    {
        /// <summary>
        /// 配置中心单据打印
        /// </summary>
        /// <param name="allData"></param>
        /// <returns></returns>
        int PrintCompound(System.Collections.ArrayList allData);

        /// <summary>
        /// 配置中心标签打印
        /// </summary>
        /// <param name="allData"></param>
        /// <returns></returns>
        int PrintCompoundLabel(System.Collections.ArrayList allData);

        /// <summary>
        /// 配置中心清单打印
        /// </summary>
        /// <param name="allData"></param>
        /// <returns></returns>
        int PrintCompoundList(System.Collections.ArrayList allData);
    }
}
