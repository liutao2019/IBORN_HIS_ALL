using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.BI
{
    /// <summary>
    /// 医保结算接口
    /// </summary>
    public interface IBalance
    {
        /// <summary>
        /// 错误
        /// </summary>
        string ErrorMsg
        {
            get;
        }

        /// <summary>
        /// 预结算
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="registerType"></param>
        /// <param name="balance"></param>
        /// <returns></returns>
        int PreBalance<T>(string registerType, T balance);

        /// <summary>
        /// 结算
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="balance"></param>
        /// <returns></returns>
        int Balance<T>(string registerType, T balance);

        /// <summary>
        /// 取消结算
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="balance"></param>
        /// <returns></returns>
        int CancelBalance<T>(string registerType, T balance);

        /// <summary>
        /// 同步医保结算结果到本地
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="registerType"></param>
        /// <param name="register"></param>
        /// <returns></returns>
        int SyncMedicalBalance<T>(string registerType, T register);

    }
}
