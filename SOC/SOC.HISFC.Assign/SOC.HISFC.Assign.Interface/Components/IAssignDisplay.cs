using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Assign.Interface.Components
{
    /// <summary>
    /// [功能描述: 门诊分诊屏显示接口]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public interface IAssignDisplay:IDisposable
    {
        /// <summary>
        /// 显示屏显内容
        /// </summary>
        void Show();

        /// <summary>
        /// 关闭
        /// </summary>
        void Close();
    }
}
