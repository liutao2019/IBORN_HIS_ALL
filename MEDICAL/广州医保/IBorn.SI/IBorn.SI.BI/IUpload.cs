using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.BI
{
    /// <summary>
    /// 医保上传接口
    /// </summary>
    public interface IUpload
    {
        /// <summary>
        /// 错误
        /// </summary>
        string ErrorMsg
        {
            get;
        }

        /// <summary>
        /// 获取需要上传的费用项目明细
        /// </summary>
        /// <param name="registerType"></param>
        /// <param name="registerID"></param>
        /// <returns></returns>
        //System.Data.DataTable GetNeedUploadFeeDetail(string registerType, string registerID);

        ///// <summary>
        ///// 获取需要上传的费用项目明细
        ///// </summary>
        ///// <param name="registerType"></param>
        ///// <param name="registerID"></param>
        ///// <returns></returns>
        //List<T> GetNeedUploadFeeDetail<T>(string registerType, string registerID);

        ///// <summary>
        ///// 费用上传及试算
        ///// </summary>
        ///// <param name="registerType"></param>
        ///// <param name="register"></param>
        ///// <param name="feeDetail"></param>
        ///// <returns></returns>
        //int UploadFee<R,T>(string registerType,R register, List<T> feeDetail);

        /// <summary>
        /// 获取需要上传的费用项目明细
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="registerType"></param>
        /// <param name="registerID"></param>
        /// <returns></returns>
        System.Data.DataTable GetNeedUploadFeeDetail(string registerType, string registerID);

        /// <summary>
        /// 获取需要上传的费用项目明细
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="registerType"></param>
        /// <param name="registerID"></param>
        /// <param name="invoiceNO"></param>
        /// <returns></returns>
        System.Data.DataTable GetNeedUploadFeeDetail(string registerType, string registerID, string invoiceNO);

        /// <summary>
        /// 获取有匹配但手工选择不上传医保的项目
        /// </summary>
        /// <param name="registerType"></param>
        /// <param name="registerID"></param>
        /// <returns></returns>
        System.Data.DataTable GetNotUploadFeeDetail(string registerType, string registerID);

        /// <summary>
        /// 费用上传
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="registerType"></param>
        /// <param name="register"></param>
        /// <param name="feeDetail"></param>
        /// <returns></returns>
        int UploadFee<R, T>(string registerType, R register, T feeDetail);

        /// <summary>
        /// 删除已上传的费用项目
        /// </summary>
        /// <param name="registerType"></param>
        /// <param name="registerID"></param>
        /// <returns></returns>
        int DeleteFee<T>(string registerType, T register);


    }
}
