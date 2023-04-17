using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer
{
    /// <summary>
    /// 用法排序：一组内可能用法不一致，此处只显示一个用法
    /// </summary>
    public class UsageCompare : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Order.OutPatient.Order order1 = x as FS.HISFC.Models.Order.OutPatient.Order;
                FS.HISFC.Models.Order.OutPatient.Order order2 = y as FS.HISFC.Models.Order.OutPatient.Order;

                if (FS.FrameWork.Function.NConvert.ToInt32(order1.Usage.ID) < FS.FrameWork.Function.NConvert.ToInt32(order2.Usage.ID))
                {
                    return 1;
                }
                else if (FS.FrameWork.Function.NConvert.ToInt32(order1.Usage.ID) == FS.FrameWork.Function.NConvert.ToInt32(order2.Usage.ID))
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        #endregion
    }
}
