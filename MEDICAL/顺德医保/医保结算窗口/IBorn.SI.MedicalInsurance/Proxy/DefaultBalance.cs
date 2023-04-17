using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.MedicalInsurance.FoShan.Proxy
{
    public class DefaultBalance : IBorn.SI.BI.IBalance
    {

        #region IBalance 成员
       
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

        int IBorn.SI.BI.IBalance.Balance<T>(string registerType, T balance)
        {
            return 1;
        }

        int IBorn.SI.BI.IBalance.CancelBalance<T>(string registerType, T balance)
        {
            return 1;
        }

        int IBorn.SI.BI.IBalance.PreBalance<T>(string registerType, T balance)
        {
            return 1;
        }
        
        int IBorn.SI.BI.IBalance.SyncMedicalBalance<T>(string registerType, T register)
        {
            return 1;
        }

        #endregion

    }
}
