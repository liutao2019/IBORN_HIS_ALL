using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Runtime.Serialization;

namespace FS.HISFC.BizLogic.Privilege.Model
{
    /// <summary>
    /// �����Ϣ
    [KnownType(typeof(User)),Serializable]
    public class NeuIdentity:IIdentity
    {
        private User _user = null;
        private bool _isAuthenticated = false;
        private string _authenticationType;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authenticationType"></param>
        /// <param name="isAuthenticated"></param>
        public NeuIdentity(User user, string authenticationType, bool isAuthenticated)
        {
            _user = user;
            _authenticationType = authenticationType;
            _isAuthenticated = isAuthenticated;
        }

        #region IIdentity ��Ա

        /// <summary>
        /// ��֤����
        /// </summary>
        
        public string AuthenticationType
        {
            get { return _authenticationType; }
            set { _authenticationType = value; }
        }

        /// <summary>
        /// �Ƿ�ͨ����֤
        /// </summary>
        
        public bool IsAuthenticated
        {
            get 
            { 
                return _isAuthenticated; 
            }

            set
            {
                _isAuthenticated = value;
            }
        }
              
        /// <summary>
        /// ��ǰ�û���Ϣ
        /// </summary>
        
        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        /// <summary>
        /// ��¼�ʺ�
        /// </summary>
        //[DataMember(Order=100)]
        public string Name
        {
            get { return _user.Account; }
            set { _user.Account = value; }
        }
        #endregion
    }
}
