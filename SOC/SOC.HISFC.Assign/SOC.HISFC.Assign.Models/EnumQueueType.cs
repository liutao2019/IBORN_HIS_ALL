using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;
using System.Collections;
using FS.FrameWork.Public;

namespace FS.SOC.HISFC.Assign.Models
{
    /// <summary>
    /// [功能描述: 队列类型枚举]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    [Serializable]
    public enum EnumQueueType
    {
        /// <summary>
        /// 自定义
        /// </summary>
        [Description("自定义队列")]
        Custom,
        /// <summary>
        /// 医生队列
        /// </summary>
        [Description("医生队列")]
        Doctor,
        /// <summary>
        /// 级别队列
        /// </summary>
        [Description("级别队列")]
        RegLevel
    }

}
