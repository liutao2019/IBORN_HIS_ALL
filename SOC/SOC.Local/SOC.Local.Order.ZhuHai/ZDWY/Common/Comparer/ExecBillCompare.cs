using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer
{
    public class ExecBillCompare : IComparer
    {
        public Hashtable hsExec = new Hashtable();

        #region IComparer 成员

        public int Compare(object x, object y)
        {
            if (string.IsNullOrEmpty((hsExec[x.ToString()] as FS.FrameWork.Models.NeuObject).User03) && string.IsNullOrEmpty((hsExec[y.ToString()] as FS.FrameWork.Models.NeuObject).User03))
            {
                return x.ToString().CompareTo(y.ToString());
            }
            else
            {
                return (hsExec[x.ToString()] as FS.FrameWork.Models.NeuObject).User03.CompareTo((hsExec[y.ToString()] as FS.FrameWork.Models.NeuObject).User03);
            }
        }

        #endregion
    }
}
