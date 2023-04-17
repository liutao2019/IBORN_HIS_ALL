using System;

namespace FoShanDiseasePay.Base
{
    /// <summary>
    /// Baseobj 的摘要说明。
    /// </summary>
    public class BaseObj
    {
        public BaseObj()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public string HospitalID = String.Empty;

        public string HospitalName = String.Empty;

        public string UserCode = string.Empty;

        public string OrgCode = string.Empty;

        public string Token = string.Empty;

        public string IPAddress = string.Empty;

        private string Sign = string.Empty;
    }
}
