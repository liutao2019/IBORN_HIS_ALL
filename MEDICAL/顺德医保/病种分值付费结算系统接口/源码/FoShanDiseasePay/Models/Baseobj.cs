using System;

namespace FoShanDiseasePay.Base
{
    /// <summary>
    /// Baseobj ��ժҪ˵����
    /// </summary>
    public class BaseObj
    {
        public BaseObj()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
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
