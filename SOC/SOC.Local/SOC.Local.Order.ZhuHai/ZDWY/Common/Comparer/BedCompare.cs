using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer
{
    public class BedCompare : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            FS.FrameWork.Models.NeuObject neuX = x as FS.FrameWork.Models.NeuObject;
            FS.FrameWork.Models.NeuObject neuY = y as FS.FrameWork.Models.NeuObject;
            string bedNoX = neuX.Name.Substring(4);
            string bedNoY = neuY.Name.Substring(4);
            return bedNoX.CompareTo(bedNoY);
        }

        #endregion
    }
}
