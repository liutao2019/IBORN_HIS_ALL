using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer
{
    /// <summary>
    /// 按使用时间排序
    /// </summary>
    public class OrderCompareByUsage : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Order.ExecOrder lExecOrder = x as FS.HISFC.Models.Order.ExecOrder;
            FS.HISFC.Models.Order.ExecOrder rExecOrder = y as FS.HISFC.Models.Order.ExecOrder;
            return lExecOrder.DateUse.CompareTo(rExecOrder.DateUse);
        }

        #endregion
    }
}
