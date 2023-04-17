using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer
{
    public class FeeCompare : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList fee1 = x as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                FS.HISFC.Models.Fee.Outpatient.FeeItemList fee2 = y as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                return string.Compare(fee1.Order.Combo.ID + FS.FrameWork.Function.NConvert.ToInt32(fee1.Item.IsMaterial).ToString() + fee1.Order.SortID.ToString(), fee2.Order.Combo.ID + FS.FrameWork.Function.NConvert.ToInt32(fee2.Item.IsMaterial).ToString() + fee2.Order.SortID.ToString());
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }
}
