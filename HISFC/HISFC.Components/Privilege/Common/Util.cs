using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using FS.HISFC.BizLogic.Privilege.Service;


namespace FS.HISFC.Components.Privilege.Common
{
    internal class Util
    {
        ////private static FS.Framework.Unity.UnityContainerFactoryNew factory;

        //Util()
        //{

        //}

        public static PrivilegeService CreateProxy()
        {
            PrivilegeService p = new PrivilegeService();
            return p;
        }
        public static string ToString(object obj)
        {
            if (obj == null) return "";

            return obj.ToString();
        }

        ///// <summary>
        ///// ���ϵͳʱ��
        ///// </summary>
        ///// <returns></returns>
        //public  DateTime GetDateTime()
        //{
 
        //    DateTime _current = DateTime.MinValue;
        //    _current = FS.Framework.Facade.Context.GetServerDateTime();
        //    return _current;
        //}

        /// <summary>
        /// Hash����
        /// </summary>
        /// <param name="orig"></param>
        /// <returns></returns>
        public static string CreateHash(string origin)
        {
            return null;// FS.Framework.Security.Cryptography.Cryptographer.CreateHash("SHA1Managed", origin);
        }

        /// <summary>
        /// Hash����
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static bool CompareHash(string newOriginPass, string oldEncryptPass)
        {
            return false;// FS.Framework.Security.Cryptography.Cryptographer.CompareHash("SHA1Managed", newOriginPass, oldEncryptPass);
        }


    }

}
