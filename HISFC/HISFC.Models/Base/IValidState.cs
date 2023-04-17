using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Base
{
    /// <summary>
    /// IInvalid<br></br>
    /// [功能描述: 实现有效性状态标识]<br></br>
    /// [创 建 者: 飞斯]<br></br>
    /// [创建时间: 2007-09-14]<br></br>
    /// </summary>
    //[System.Serializable]
    public interface IValidState
    {
        /// <summary>
        /// 有效性标识枚举
        /// </summary>
        FS.HISFC.Models.Base.EnumValidState ValidState
        {
            get;
            set;
        }
    }
}
