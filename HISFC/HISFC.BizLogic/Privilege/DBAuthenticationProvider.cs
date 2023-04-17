using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.BizLogic.Privilege.Model;


namespace FS.HISFC.BizLogic.Privilege
{
    /// <summary>
    /// ��֤����
    /// </summary>
    public class DBAuthenticationProvider
    {
       
        
        /// <summary>
        /// ��֤����{D515E09B-E299-47e0-BF19-EDFDB6E4C775}
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public NeuIdentity Authenticate(string name, string password, string domain)
        {
            //�����ʻ���û�����Ϣ
            User _user = SecurityService.GetUserByAccount(name);
            if (_user == null)
            {
                throw new Exception("û�и��û�ע����Ϣ!");
            }
            //{D515E09B-E299-47e0-BF19-EDFDB6E4C775}
            //string pass = FS.HisCrypto.HisDecrypt.Encrypt(password);
            string pass = FS.HisCrypto.DESCryptoService.DESEncrypt(password,FS.FrameWork.Management.Connection.DESKey);


            bool _isMatch;
            //�ж������Ƿ����
            _isMatch = string.Equals(_user.Password, pass);
                        
            if (!_isMatch)
            {
                throw new Exception("�������벻��ȷ!");
            }

            NeuIdentity _identity = new NeuIdentity(_user, "DAO", true);

            return _identity;
        }




    }
}
