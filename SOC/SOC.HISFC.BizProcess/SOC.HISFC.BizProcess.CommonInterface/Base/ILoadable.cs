using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.CommonInterface
{
    /// <summary>
    /// [功能描述: 加载数据接口]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public interface ILoadable
    {
        /// <summary>
        /// 加载界面
        /// </summary>
        /// <returns></returns>
        int Load();
    }
}
