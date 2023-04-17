using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.BI
{
    /// <summary>
    /// 医保登记接口
    /// </summary>
    public interface IRADT
    {
        /// <summary>
        /// 错误
        /// </summary>
        string ErrorMsg
        {
            get;
        }

        /// <summary>
        /// 获取患者信息
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        T GetPatient<T>(string registerType, T register);

        /// <summary>
        /// 验证登记信息
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        int Verification<T>(string registerType, T register);

        /// <summary>
        /// 登记
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        int Register<T>(string registerType, T register);

        /// <summary>
        /// 取消登记
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        int CancelRegister<T>(string registerType, T register);


        /// <summary>
        /// 变更信息
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        int ChangePatient<T>(string registerType, T register);

        /// <summary>
        /// 出院登记
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="register"></param>
        /// <returns></returns>
        int LeaveRegister<T>(string registerType, T register);

        /// <summary>
        /// 取消出院登记（召回）
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        int CancelLeaveRegister<T>(string registerType, T register);
    }
}
