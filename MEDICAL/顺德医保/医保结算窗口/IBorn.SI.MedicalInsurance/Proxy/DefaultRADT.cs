using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBorn.SI.BI;

namespace IBorn.SI.MedicalInsurance.FoShan.Proxy
{
    public class DefaultRADT:IRADT
    {

        #region IRADT 成员
        private string errorMsg;
        /// <summary>
        /// 错误提示信息
        /// </summary>
        public string ErrorMsg
        {
            get
            {
                return this.errorMsg;
            }
        }

        int IBorn.SI.BI.IRADT.CancelLeaveRegister<T>(string registerType, T register)
        {
            return 1;
        }

        int IBorn.SI.BI.IRADT.CancelRegister<T>(string registerType, T register)
        {
            return 1;
        }

        int IBorn.SI.BI.IRADT.ChangePatient<T>(string registerType, T register)
        {
            return 1;
        }

        T IBorn.SI.BI.IRADT.GetPatient<T>(string registerType, T register)
        {
            return register;
        }

        int IBorn.SI.BI.IRADT.LeaveRegister<T>(string registerType, T register)
        {
            return 1;
        }

        int IBorn.SI.BI.IRADT.Register<T>(string registerType, T register)
        {
            return 1;
        }

        int IBorn.SI.BI.IRADT.Verification<T>(string registerType, T register)
        {
            return 1;
        }

        #endregion
    }
}
