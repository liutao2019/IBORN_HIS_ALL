using System;
using System.Data;
using System.Configuration;

namespace API.GZSI.Common
{
    /// <summary>
    /// 常数定义类 by chen.fch@2018-01-15
    /// </summary>
    public class Constants
    {
        #region 时间转换格式

        /// <summary>
        /// 时间格式：HH:mm
        /// </summary>
        public static readonly string FORMAT_TIME = "HH:mm";

        /// <summary>
        /// 时间格式：HH:mm:ss
        /// </summary>
        public static readonly string FORMAT_TIME1 = "HH:mm:ss";

        /// <summary>
        /// 日期格式：yyyyMMdd
        /// </summary>
        public static readonly string FORMAT_DATE = "yyyyMMdd";

        /// <summary>
        /// 日期格式：yyyy.MM.dd
        /// </summary>
        public static readonly string FORMAT_DATE1 = "yyyy.MM.dd";

        /// <summary>
        /// 日期格式：yyyy年M月d日
        /// </summary>
        public static readonly string FORMAT_DATE2 = "yyyy年M月d日";

        /// <summary>
        /// 日期格式：yyyy-MM-dd
        /// </summary>
        public static readonly string FORMAT_DATE3 = "yyyy-MM-dd";

        /// <summary>
        /// 时间格式：yyyyMMddHHmmss
        /// </summary>
        public static readonly string FORMAT_DATETIME = "yyyyMMddHHmmss";

        /// <summary>
        /// 时间格式：yyyy-MM-dd HH:mm:ss
        /// </summary>
        public static readonly string FORMAT_DATETIME1 = "yyyy-MM-dd HH:mm:ss";

        #endregion 

        #region 医院账户信息

        /// <summary>
        /// 医院账户维护文件
        /// </summary>
        public static readonly string GZSI_USER_INFO_INI = "GZSI_UserInfo.ini";
        
        /// <summary>
        /// 医院维护文件节点
        /// </summary>
        public static readonly string GZSI_USERINFO = "GZSI_USERINFO";
        /// <summary>
        /// 医院url
        /// </summary>
        public static readonly string GZSI_URL = "GZSI_URL";
        /// <summary>
        /// 医院账户ID
        /// </summary>
        public static readonly string GZSI_USER_ID = "USER_ID";
        /// <summary>
        /// 医院账户Password
        /// </summary>
        public static readonly string GZSI_USER_PASSWORD = "USER_PASSWORD";
        /// <summary>
        /// 医院应用编码
        /// </summary>
        public static readonly string GZSI_APPCODE = "GZSI_APPCODE";
        /// <summary>
        /// 医院秘钥
        /// </summary>
        public static readonly string GZSI_SECRETKEY = "GZSI_SECRETKEY";
        /// <summary>
        /// 签到流水号
        /// </summary>
        public static readonly string GZSI_USER_SIGNNO = "GZSI_SIGNNO";
        /// <summary>
        /// 签到时间
        /// </summary>
        public static readonly string GZSI_SIGNTIME = "GZSI_SIGNTIME";
        /// <summary>
        /// 签到操作人
        /// </summary>
        public static readonly string GZSI_SIGNOPER = "GZSI_SIGNOPER";
        /// <summary>
        /// 签到操作人
        /// </summary>
        public static readonly string GZSI_READCARDURL = "GZSI_READCARDURL";

        #endregion 

        #region 码表字典定义
        
        /// <summary>
        /// 码表字典前缀
        /// </summary>
        public static readonly string GZSI_CODE_PREFIX = "GZSI_";

        /// <summary>
        /// 码表清单
        /// </summary>
        public static readonly string GZSI_CODELIST = "GZSI_CODE";

        #endregion
    }

}
