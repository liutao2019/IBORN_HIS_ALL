using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Equipment
{
    /// <summary>
    /// IApplyPrint<br></br>
    /// [功能描述: 设备购置申请打印接口]<br></br>
    /// [创 建 者: 耿晓雷]<br></br>
    /// [创建时间: 2007-11-29]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public interface IApplyPrint
    {

        void SetPrintValue(FS.HISFC.Models.Equipment.BuyApply buyApply);

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <returns>>成功 1 失败 -1</returns>
        int PrintView();

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        int Print();
    }
}
