using System;

namespace FoShanDiseasePay.Setting
{
    /// <summary>
    /// neuSetting 的摘要说明。
    /// </summary>
    public class SettingObject
    {
        public SettingObject()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// his数据库最大连接数
        /// </summary>
        public int MaxConCount = 10;

        /// <summary>
        /// sql列表
        /// </summary>
        public System.Collections.ArrayList SqlList = new System.Collections.ArrayList();

        /// <summary>
        /// 常数列表
        /// </summary>
        public System.Collections.ArrayList ContentList = new System.Collections.ArrayList();

        /// <summary>
        /// HIS连接串
        /// </summary>
        public string SQLConnectionString = String.Empty;

        /// <summary>
        /// Pacs连接串
        /// </summary>
        public string PacsConnectionString = String.Empty;

        /// <summary>
        /// B超链接串
        /// </summary>
        public string RadioConnectionString = String.Empty;

        /// <summary>
        /// LIS连接串
        /// </summary>
        public string LISConnectionString = String.Empty;

        /// <summary>
        /// 前置机连接串
        /// </summary>
        public string FoShanSIConnectionString = String.Empty;

        /// <summary>
        /// web service地址
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


        #region 新接口

        /// <summary>
        /// web service地址
        /// </summary>
        public string NewWebServiceAddress = string.Empty;

        public string NewUserID = string.Empty;

        public string NewUserCode = string.Empty;

        public string NewOrgCode = string.Empty;

        public string NewToken = string.Empty;

        public string NewSign = string.Empty;
        #endregion 

        /// <summary>
        /// 开关
        /// </summary>
        public bool SQLByXMl = false;


    }
}
