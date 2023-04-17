using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.OrderInterface.Common
{
    /// <summary>
    /// 医嘱取整接口
    /// </summary>
    public interface IOrderSplit
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        string ErrInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 获取药品的取整类型
        /// </summary>
        /// <param name="index">0 门诊；1 住院临嘱；2 住院长嘱</param>
        /// <param name="order"></param>
        /// <returns></returns>
        string GetSplitType(int index, FS.HISFC.Models.Order.Order order); 

        /// <summary>
        /// 门诊处方和住院临嘱取整
        /// </summary>
        /// <param name="orderBase"></param>
        /// <returns></returns>
        int ComputeOrderQty(FS.HISFC.Models.Order.Order order);

        /// <summary>
        /// 住院执行档收费取整
        /// </summary>
        /// <param name="execOrder">执行档信息</param>
        /// <param name="feeFlag">0 不收费；1 按照取整数量收费；2 不处理取整，按原有流程收费</param>
        /// <param name="feeNum">收费数量</param>
        /// <param name="phaNum">发药数量</param>
        /// <returns></returns>
        int ComputeOrderQty(FS.HISFC.Models.Order.ExecOrder execOrder,ref string feeFlag, ref decimal feeNum, ref decimal phaNum);
    }
}
