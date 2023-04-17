using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer
{
    public class ExecTimeCompare : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            return FS.FrameWork.Function.NConvert.ToDateTime(x).CompareTo(FS.FrameWork.Function.NConvert.ToDateTime(y));
        }

        #endregion
    }
}
