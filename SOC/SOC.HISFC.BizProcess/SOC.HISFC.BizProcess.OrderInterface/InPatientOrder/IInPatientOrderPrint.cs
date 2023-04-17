using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInPatientOrderPrint
    {
        #region 实现该接口的类有 通过index区分
 

        #endregion

        /// <summary>
        /// 错误信息
        /// </summary>
        string ErrInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 门诊医生站本地化单据打印
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="type"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <param name="IsReprint"></param>
        /// <returns></returns>
        int PrintInPatientOrderBill(FS.HISFC.Models.RADT.PatientInfo patientInfo, string type, FS.FrameWork.Models.NeuObject deptNeuObject, FS.FrameWork.Models.NeuObject doctNeuObject, System.Collections.ArrayList alOrder, bool IsReprint);

        /// <summary>
        /// 门诊医生站本地化单据打印<泛型>
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="type"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="IList"></param>
        /// <param name="IsReprint">是否是补打</param>
        /// <returns></returns>
        int PrintInPatientOrderBill(FS.HISFC.Models.RADT.PatientInfo patientInfo, string type, FS.FrameWork.Models.NeuObject deptNeuObject, FS.FrameWork.Models.NeuObject doctNeuObject, IList<FS.HISFC.Models.Order.Order> IList, bool IsReprint);
    }
}
