using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.CommonInterface
{
    /// <summary>
    /// [功能描述: 有效性接口]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public interface IValidabe
    {
        /// <summary>
        /// 输入是否有效
        /// </summary>
        /// <returns></returns>
        bool InputValid();
    }
}
