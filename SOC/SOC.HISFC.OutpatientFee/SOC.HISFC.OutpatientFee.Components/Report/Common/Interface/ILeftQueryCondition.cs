using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Interface
{
    public interface ILeftQueryCondition : ITopQueryCondition
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="common"></param>
        /// <returns></returns>
        Object GetValues(QueryControl common);

        /// <summary>
        /// 获取文本值
        /// </summary>
        /// <param name="common"></param>
        /// <returns></returns>
        Object GetTexts(QueryControl common);
    }
}
