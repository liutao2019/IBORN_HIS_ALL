using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer
{
    /// <summary>
    /// 按开嘱时间排序
    /// </summary>
    public class OrderCompareByMoDate : IComparer
    {

        #region IComparer 成员

        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Order.ExecOrder lExecOrder = x as FS.HISFC.Models.Order.ExecOrder;
            FS.HISFC.Models.Order.ExecOrder rExecOrder = y as FS.HISFC.Models.Order.ExecOrder;
            //return lExecOrder.Order.MOTime.CompareTo(rExecOrder.Order.MOTime);
            if (lExecOrder.Order.MOTime.ToString() == rExecOrder.Order.MOTime.ToString())
            {
                string lExecOrderCombID = lExecOrder.Order.Usage.ID + lExecOrder.Order.Combo.ID;
                string rExecOrderCombID = rExecOrder.Order.Usage.ID + rExecOrder.Order.Combo.ID;
                if (lExecOrderCombID == rExecOrderCombID)
                {
                    int xSortid = FS.FrameWork.Function.NConvert.ToInt32(lExecOrder.Order.SortID);
                    int ySortid = FS.FrameWork.Function.NConvert.ToInt32(rExecOrder.Order.SortID);
                    if (xSortid == ySortid)
                    {
                        int xOrderid = FS.FrameWork.Function.NConvert.ToInt32(lExecOrder.Order.ID);
                        int yOrderid = FS.FrameWork.Function.NConvert.ToInt32(rExecOrder.Order.ID);
                        if (xOrderid == yOrderid)
                        {
                            string xUseTime = lExecOrder.DateUse.ToString();
                            string yUseTime = rExecOrder.DateUse.ToString();
                            return xUseTime.CompareTo(yUseTime);
                        }
                        else
                        {
                            return xOrderid.CompareTo(yOrderid);
                        }
                    }
                    else
                    {
                        return xSortid.CompareTo(ySortid);
                    }
                }
                else
                {
                    return lExecOrderCombID.CompareTo(rExecOrderCombID);
                }
            }
            else
            {
                return lExecOrder.Order.MOTime.CompareTo(rExecOrder.Order.MOTime);
            }
        }

        #endregion
    }
}
