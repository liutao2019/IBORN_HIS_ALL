using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.OrderInterface.Common
{
    /// <summary>
    /// 修改医嘱（处方）接口
    /// </summary>
    public interface IModifyOrder
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
        /// 修改门诊处方实体
        /// </summary>
        /// <param name="outOrder"></param>
        /// <param name="changedField">变化字段</param>
        /// <returns></returns>
        int ModifyOutOrder(FS.HISFC.Models.Order.OutPatient.Order outOrder, string changedField);

        /// <summary>
        /// 修改住院医嘱实体
        /// </summary>
        /// <param name="outOrder"></param>
        /// <param name="changedField">变化字段</param>
        /// <returns></returns>
        int ModifyInOrder(FS.HISFC.Models.Order.Inpatient.Order inOrder, string changedField);
    }
}
