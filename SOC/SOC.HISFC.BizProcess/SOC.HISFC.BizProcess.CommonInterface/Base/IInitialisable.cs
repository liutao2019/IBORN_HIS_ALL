using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.CommonInterface
{
    /// <summary>
    /// [功能描述: 初始化接口]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public interface IInitialisable
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        int Init();
    }
}
