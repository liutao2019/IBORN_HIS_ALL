using System;

namespace FoShanDiseasePay.Setting
{
    /// <summary>
    /// neuSetting ��ժҪ˵����
    /// </summary>
    public class SettingObject
    {
        public SettingObject()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        /// <summary>
        /// his���ݿ����������
        /// </summary>
        public int MaxConCount = 10;

        /// <summary>
        /// sql�б�
        /// </summary>
        public System.Collections.ArrayList SqlList = new System.Collections.ArrayList();

        /// <summary>
        /// �����б�
        /// </summary>
        public System.Collections.ArrayList ContentList = new System.Collections.ArrayList();

        /// <summary>
        /// HIS���Ӵ�
        /// </summary>
        public string SQLConnectionString = String.Empty;

        /// <summary>
        /// Pacs���Ӵ�
        /// </summary>
        public string PacsConnectionString = String.Empty;

        /// <summary>
        /// B�����Ӵ�
        /// </summary>
        public string RadioConnectionString = String.Empty;

        /// <summary>
        /// LIS���Ӵ�
        /// </summary>
        public string LISConnectionString = String.Empty;

        /// <summary>
        /// ǰ�û����Ӵ�
        /// </summary>
        public string FoShanSIConnectionString = String.Empty;

        /// <summary>
        /// web service��ַ
        /// </summary>
        public string WebServiceAddress = string.Empty;

        public string HospitalID = String.Empty;

        public string HospitalName = String.Empty;

        public string UserID = string.Empty;

        public string UserCode = string.Empty;

        public string OrgCode = string.Empty;

        public string Token = string.Empty;

        public string IPAddress = string.Empty;

        public string Sign = string.Empty;


        #region �½ӿ�

        /// <summary>
        /// web service��ַ
        /// </summary>
        public string NewWebServiceAddress = string.Empty;

        public string NewUserID = string.Empty;

        public string NewUserCode = string.Empty;

        public string NewOrgCode = string.Empty;

        public string NewToken = string.Empty;

        public string NewSign = string.Empty;
        #endregion 

        /// <summary>
        /// ����
        /// </summary>
        public bool SQLByXMl = false;


    }
}
