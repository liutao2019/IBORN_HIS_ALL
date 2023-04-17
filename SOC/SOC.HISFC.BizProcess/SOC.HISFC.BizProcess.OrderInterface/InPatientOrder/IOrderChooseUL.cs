using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder
{
    /// <summary>
    /// 医生站开立检验项目接口
    /// </summary>
    public interface IOrderChooseUL
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        string Err
        {
            get;
            set;
        }

        /// <summary>
        /// 选择检验项目
        /// </summary>
        /// <param name="alOrders">选择的医嘱数组</param>
        /// <returns>错误返回-1</returns>
        int GetChooseUL(ref ArrayList alOrders);
    }
}
