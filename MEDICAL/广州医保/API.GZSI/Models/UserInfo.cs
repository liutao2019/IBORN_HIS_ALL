using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace API.GZSI.Models
{
    /// <summary>
    /// 医院账号，单例模式（多线程）安全单例 by chen.fch@20191024
    /// </summary>
    [Serializable]
    public sealed class UserInfo
    {
        #region 属性

        /// <summary>
        /// 接口请求链接
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 自费上传接口请求链接
        /// </summary>
        public string zf_url { get; set; }

        /// <summary>
        /// 医院账户ID
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 医院账户Password
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 医院应用编码
        /// </summary>
        public string app_code { get; set; }

        /// <summary>
        /// 医保私钥
        /// </summary>
        public string secret_key { get; set; }

        /// <summary>
        /// 签到流水号
        /// </summary>
        public string sign_no { get; set; }

        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime sign_time { get; set; }

        /// <summary>
        /// 签到操作人
        /// </summary>
        public string sign_oper { get; set; }

        /// <summary>
        /// 读卡地址
        /// </summary>
        public string readcard_url { get; set; }

        /// <summary>
        /// 医疗机构编码
        /// </summary>
        public string fixmedins_code { get; set; }

        /// <summary>
        /// 参保地
        /// </summary>
        public string insuplc { get; set; }
        #endregion

        #region 单例
        private static UserInfo _instance = null;
        private static readonly object obj = new object();
        public static UserInfo Instance
        {
            get
            {

                if (_instance == null)
                {
                    lock (obj)
                    {
                        _instance = new UserInfo();
                    }
                }

                return _instance;

            }
        }
        #endregion 单例

        /// <summary>
        /// 构造函数
        /// </summary>
        private UserInfo()
        {
            string iniFile = AppDomain.CurrentDomain.BaseDirectory + @"\Plugins\SI\" + Common.Constants.GZSI_USER_INFO_INI;
            if (!File.Exists(iniFile))
            {
                throw new Exception("医院账号配置文件不存在！请确认是否维护" + iniFile + "文件。");
            }
            //请求地址
            this.url = FS.FrameWork.WinForms.Classes.Function.ReadPrivateProfile(
                Common.Constants.GZSI_USERINFO, 
                Common.Constants.GZSI_URL, 
                iniFile);
            //自费接口请求地址
            this.zf_url = FS.FrameWork.WinForms.Classes.Function.ReadPrivateProfile(
                Common.Constants.GZSI_USERINFO,
                Common.Constants.GZSI_ZFURL,
                iniFile);
            //医院账号
            this.userId = FS.FrameWork.WinForms.Classes.Function.ReadPrivateProfile(
                Common.Constants.GZSI_USERINFO, 
                Common.Constants.GZSI_USER_ID, 
                iniFile);
            //医院密码
            this.password = FS.FrameWork.WinForms.Classes.Function.ReadPrivateProfile(
                Common.Constants.GZSI_USERINFO, 
                Common.Constants.GZSI_USER_PASSWORD, 
                iniFile);
            //app编号
            this.app_code = FS.FrameWork.WinForms.Classes.Function.ReadPrivateProfile(
                Common.Constants.GZSI_USERINFO, 
                Common.Constants.GZSI_APPCODE, 
                iniFile);
            //医院秘钥
            this.secret_key = FS.FrameWork.WinForms.Classes.Function.ReadPrivateProfile(
                Common.Constants.GZSI_USERINFO, 
                Common.Constants.GZSI_SECRETKEY, 
                iniFile);
            //签到流水号
            this.sign_no = FS.FrameWork.WinForms.Classes.Function.ReadPrivateProfile(
                Common.Constants.GZSI_USERINFO, 
                Common.Constants.GZSI_USER_SIGNNO, 
                iniFile);
            //签到时间
            this.sign_time = FS.FrameWork.Function.NConvert.ToDateTime 
            (FS.FrameWork.WinForms.Classes.Function.ReadPrivateProfile(
                Common.Constants.GZSI_USERINFO, 
                Common.Constants.GZSI_SIGNTIME, 
                iniFile));
            //签到人
            this.sign_oper = FS.FrameWork.WinForms.Classes.Function.ReadPrivateProfile(
                Common.Constants.GZSI_USERINFO, 
                Common.Constants.GZSI_SIGNOPER,
                iniFile);
            //读卡地址
            this.readcard_url = FS.FrameWork.WinForms.Classes.Function.ReadPrivateProfile(
                Common.Constants.GZSI_USERINFO,
                Common.Constants.GZSI_READCARDURL,
                iniFile);
            //医疗机构编码
            this.fixmedins_code = FS.FrameWork.WinForms.Classes.Function.ReadPrivateProfile(
                Common.Constants.GZSI_USERINFO,
                Common.Constants.FIXMEDINS_CODE,
                iniFile);
        }

        /// <summary>
        /// 保存签到信息
        /// </summary>
        public void SaveSign()
        {
            try
            {
                string iniFile = AppDomain.CurrentDomain.BaseDirectory + @"\Plugins\SI\" + Common.Constants.GZSI_USER_INFO_INI;
                //写入签到流水号
                FS.FrameWork.WinForms.Classes.Function.WritePrivateProfile(
                       Common.Constants.GZSI_USERINFO,
                       Common.Constants.GZSI_USER_SIGNNO,
                       this.sign_no, 
                       iniFile);
                //写入签到时间
                FS.FrameWork.WinForms.Classes.Function.WritePrivateProfile(
                       Common.Constants.GZSI_USERINFO,
                       Common.Constants.GZSI_SIGNTIME,
                       this.sign_time.ToString(),
                       iniFile);
                //写入签到人
                FS.FrameWork.WinForms.Classes.Function.WritePrivateProfile(
                       Common.Constants.GZSI_USERINFO,
                       Common.Constants.GZSI_SIGNOPER,
                       this.sign_oper, 
                       iniFile);
            }
            catch { }
        }
    }
}
