using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.BI
{
    /// <summary>
    /// 医保对照接口
    /// </summary>
    public interface ICompare
    {
        /// <summary>
        /// 错误
        /// </summary>
        string ErrorMsg
        {
            get;
        }

        ///// <summary>
        ///// 下载医保中心项目目录
        ///// </summary>
        ///// <param name="medicalType"></param>
        ///// <returns></returns>
        //int DownloadCenterItem(string medicalType);

        ///// <summary>
        ///// 上传对照信息
        ///// </summary>
        ///// <param name="medicalType"></param>
        ///// <param name="compareItem"></param>
        ///// <returns></returns>
        //int ApproveCompareItem(string medicalType, List<WF.Expense.Models.Common.MedicalCompare> compareItem);

        ///// <summary>
        ///// 取消对照信息
        ///// </summary>
        ///// <param name="medicalType"></param>
        ///// <param name="compareItem"></param>
        ///// <returns></returns>
        //int CancelCompareItem(string medicalType, List<WF.Expense.Models.Common.MedicalCompare> compareItem);

        ///// <summary>
        ///// 下载医保中心处理对照信息结果：通过审批；不通过审批
        ///// </summary>
        ///// <param name="medicalType"></param>
        ///// <param name="compareItem"></param>
        ///// <returns></returns>
        //int DownloadCompareItem(WF.Common.Base.BaseObject medicalType, ref List<WF.Expense.Models.Common.MedicalCompare> compareItem);

    }
}
