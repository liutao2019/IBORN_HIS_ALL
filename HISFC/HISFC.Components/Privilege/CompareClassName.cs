using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Components.Privilege
{
    public class CompareClassName:System.Collections.IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            return string.Compare(x.ToString(), y.ToString());
        }

        #endregion
    }
}
