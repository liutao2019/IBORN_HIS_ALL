using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.BizLogic.Privilege.Model;


namespace FS.HISFC.BizLogic.Privilege
{
    /// <summary>
    /// 认证工厂
    /// </summary>
    public class DBAuthenticationProvider
    {
       
        
        /// <summary>
        /// 认证管理{D515E09B-E299-47e0-BF19-EDFDB6E4C775}
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public NeuIdentity Authenticate(string name, string password, string domain)
        {
            //根据帐户获得患者信息
            User _user = SecurityService.GetUserByAccount(name);
            if (_user == null)
            {
                throw new Exception("没有该用户注册信息!");
            }
            //{D515E09B-E299-47e0-BF19-EDFDB6E4C775}
            //string pass = FS.HisCrypto.HisDecrypt.Encrypt(password);
            string pass = FS.HisCrypto.DESCryptoService.DESEncrypt(password,FS.FrameWork.Management.Connection.DESKey);


            bool _isMatch;
            //判断密码是否相符
            _isMatch = string.Equals(_user.Password, pass);
                        
            if (!_isMatch)
            {
                throw new Exception("输入密码不正确!");
            }

            NeuIdentity _identity = new NeuIdentity(_user, "DAO", true);

            return _identity;
        }




    }
}
